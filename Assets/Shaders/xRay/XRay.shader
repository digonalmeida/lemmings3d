// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Xray/Transparent" {
	Properties {
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_Color ("Color RGBA", Color) = (1,1,1,1)
		_ShadowStrength ("Shadow Strength", Range (0, 1)) = 1
	}
 
	SubShader 
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		LOD 100
     
		Cull Back
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
     
		/**
		
		Pass{
			Blend SrcAlpha OneMinusSrcAlpha
			Name "ShadowPass"
			Tags {"LightMode" = "ForwardBase"}
 
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fwdbase
     
				#include "UnityCG.cginc"
				#include "AutoLight.cginc"
				struct v2f
				{
					float4 pos : SV_POSITION;
					LIGHTING_COORDS(0,1)
				};
     
				fixed _ShadowStrength;
				v2f vert (appdata_full v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos (v.vertex);
					TRANSFER_VERTEX_TO_FRAGMENT(o);
					return o;
				}
				fixed4 frag (v2f i) : COLOR
				{
					fixed atten = LIGHT_ATTENUATION(i);
					fixed shadowalpha = (1.0 - atten) * _ShadowStrength;
					return fixed4(0.0, 0.0, 0.0, shadowalpha);
				}
			ENDCG
		}

		*/


		Pass {  
	  
			Name "Geometry"

			Stencil{
				Ref 2
				Comp NotEqual
				Pass Replace
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
             
			#include "UnityCG.cginc"
 
			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};
 
			struct v2f {
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
			};
 
			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;
             
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
             
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.texcoord) * _Color;
				col.a = _Color.a;
				return col;
			}
			ENDCG

		}


		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"

	}
 
}
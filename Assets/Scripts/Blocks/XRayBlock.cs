using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRayBlock : MonoBehaviour {

    public bool canBeTranslucent = true;
    public List<RenderersMaterials> xrayrenderers;
    public Transform propsParents;
    private bool isTranslucent = false;

    private void Update()
    {

        if (!canBeTranslucent)
            return;

        if (!isTranslucent)
            MakeNotTranslucent();

        if (isTranslucent)
            isTranslucent = false;

    }


    private void MakeNotTranslucent()
    {
        foreach (RenderersMaterials rm in xrayrenderers)
        {
            rm.MakeNotTranslucent();
        }

        if(propsParents!=null) propsParents.gameObject.SetActive(true);
    }

    public void MakeTranslucent()
    {
        if (!canBeTranslucent)
            return;

        foreach (RenderersMaterials rm in xrayrenderers)
        {
            rm.MakeTranslucent();
        }

        if (propsParents != null) propsParents.gameObject.SetActive(false);

        isTranslucent = true;
    }

    public void MakeShadowOnly()
    {
        if (!canBeTranslucent)
            return;

        foreach (RenderersMaterials rm in xrayrenderers)
        {
            rm.MakeShadowOnly();
        }

        if (propsParents != null) propsParents.gameObject.SetActive(false);

        isTranslucent = true;
    }

    [Serializable]
    public class RenderersMaterials {
        public List<Renderer> renderers;
        public Material translucentMaterial;
        public Material notTranslucentMaterial;

        public void MakeTranslucent()
        {
            foreach (Renderer r in renderers)
            {
                r.material = translucentMaterial;
            }
        }

        public void MakeNotTranslucent()
        {
            foreach (Renderer r in renderers)
            {
                r.material = notTranslucentMaterial;
                r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
        }

        public void MakeShadowOnly()
        {
            foreach (Renderer r in renderers)
            {
                r.material = notTranslucentMaterial;
                r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
        }
    }
}

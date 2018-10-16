using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRayBlock : MonoBehaviour {

    public bool canBeTranslucent = true;
    public Material materialNotTranslucent;
    public Material materialTranslucent;
    private Renderer objRenderer;
    private bool isTranslucent = false;

    private void Awake()
    {
        objRenderer = GetComponent<Renderer>();
    }

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
        objRenderer.material = materialNotTranslucent;
        objRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }

    public void MakeTranslucent()
    {
        if (!canBeTranslucent)
            return;

        objRenderer.material = materialTranslucent;
        isTranslucent = true;

    }

    public void MakeShadowOnly()
    {
        if (!canBeTranslucent)
            return;

        objRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        isTranslucent = true;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRayBlock : MonoBehaviour {

    public bool canBeTranslucent = true;
    public Material materialNotTranslucent;
    public Material materialTranslucent;
    private Renderer renderer;
    private bool isTranslucent = false;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
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
        renderer.material = materialNotTranslucent;
    }

    internal void MakeTranslucent()
    {
        if (!canBeTranslucent)
            return;

        renderer.material = materialTranslucent;
        isTranslucent = true;

    }
}

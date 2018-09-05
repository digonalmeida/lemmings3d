using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRayPointer : MonoBehaviour {


    private HighlightPointer highlightPointerSCript;

    [Header("X-Ray Check")]
    public LayerMask wallLayer;
    public float raydistance = 100;
    public float xRayRadius = 0.75f;
    public float thresholdAngle = 45;

    private void Awake()
    {
        highlightPointerSCript = GetComponent<HighlightPointer>();
    }

    void Update () {

        RaycastForWalls();

    }


    void RaycastForWalls()
    {

        if (highlightPointerSCript.isHighlighting)
        {
            
            Collider[] hits = Physics.OverlapCapsule(highlightPointerSCript.highlightedObject.transform.position, Camera.main.transform.position, xRayRadius, wallLayer);

            foreach (Collider hit in hits)
            {
                XRayBlock tb = hit.gameObject.GetComponent<XRayBlock>();
                if (tb != null && Vector3.Angle(Camera.main.transform.position - highlightPointerSCript.highlightedObject.transform.position, tb.transform.position - highlightPointerSCript.highlightedObject.transform.position) < thresholdAngle)
                {
                    tb.MakeTranslucent();
                }
            }


        }


    }

}

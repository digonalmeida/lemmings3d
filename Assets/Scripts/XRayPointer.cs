using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRayPointer : MonoBehaviour {


    private HighlightPointer highlightPointerSCript;

    [Header("X-Ray Check")]
    public LayerMask wallLayer;
    public float raydistance = 100;
    public float xRayRadius = 1;


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

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.CapsuleCastAll(ray.origin, ray.origin + ray.direction * raydistance, xRayRadius, ray.direction * raydistance, raydistance, wallLayer);

            foreach (RaycastHit hit in hits)
            {
                XRayBlock tb = hit.collider.gameObject.GetComponent<XRayBlock>();
                if (tb != null && tb.transform.position.y > highlightPointerSCript.highlightedObject.transform.position.y)
                {
                    tb.MakeTranslucent();
                }
            }


        }


    }

}

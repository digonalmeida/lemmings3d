using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRayPointer : MonoBehaviour {


    private HighlightPointer highlightPointerSCript;

    [Header("X-Ray Check")]
    public LayerMask wallLayer;
    public LayerMask lemmingSearchLayer;
    public float raydistance = 100;
    public float lemmingSearchRayRadius = 2f;
    public float xRayRadius = 0.75f;
    public float thresholdAngle = 45;

    private Ray ray;
    private Collider[] lemmingsColliders;
    private Collider[] blocksColiders;

    private void Awake()
    {
        highlightPointerSCript = GetComponent<HighlightPointer>();
    }

    void Update () {

        RaycastForWalls();

    }

    void RaycastForWalls()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        lemmingsColliders = Physics.OverlapCapsule(ray.origin, ray.origin + ray.direction * raydistance, lemmingSearchRayRadius, lemmingSearchLayer, QueryTriggerInteraction.Collide);

        foreach (Collider col in lemmingsColliders)
        {
            blocksColiders = Physics.OverlapCapsule(col.bounds.center, Camera.main.transform.position, xRayRadius, wallLayer);

            foreach (Collider hit in blocksColiders)
            {
                XRayBlock tb = hit.gameObject.GetComponent<XRayBlock>();
                if (tb != null && Vector3.Angle(Camera.main.transform.position - col.bounds.center, tb.transform.position - col.bounds.center) < thresholdAngle)
                {
                    tb.MakeTranslucent();
                }
            }
        }

        /*/
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
        /*/


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRayPointer : MonoBehaviour {

    public float raydistance = 100;
    public LayerMask wallLayer;
    public float xRayRadius = 1;

	void Update () {
        RaycastForWalls();
    }


    void RaycastForWalls()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * raydistance);
        RaycastHit[] hits = Physics.CapsuleCastAll(ray.origin, ray.origin + ray.direction * raydistance, xRayRadius, ray.direction * raydistance, raydistance, wallLayer);

        foreach (RaycastHit hit in hits)
        {
            XRayBlock tb = hit.collider.gameObject.GetComponent<XRayBlock>();

            if (tb != null)
            {
                tb.MakeTranslucent();
            }
            
            
        }

    }
}

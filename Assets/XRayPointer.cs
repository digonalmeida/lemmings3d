using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRayPointer : MonoBehaviour {

    public enum PointerType {
        CapsuleCast,
        CapsuleCollision
    }

    public PointerType pointerType = PointerType.CapsuleCast;
    public float raydistance = 100;
    public LayerMask wallLayer;
    public float xRayRadius = 1;
    public Transform capsuleObject;

	void Update () {
        
        capsuleObject.gameObject.SetActive(false);

        switch (pointerType)
        {
            case PointerType.CapsuleCast:
                RaycastForWalls();
                break;
            case PointerType.CapsuleCollision:
                CollideForWalls();
                break;
        }

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

    void CollideForWalls()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * raydistance);
        Collider[] cols = Physics.OverlapCapsule(ray.origin, ray.origin + ray.direction * raydistance, xRayRadius, wallLayer);

        if (cols.Length < 1)
            return;

        capsuleObject.gameObject.SetActive(true);
        capsuleObject.position = ray.origin + ray.direction * raydistance / 2;
        capsuleObject.rotation = Quaternion.LookRotation(Quaternion.AngleAxis(90,Camera.main.transform.right) * ray.direction);
        //capsuleObject.rotation = Quaternion.LookRotation(ray.direction);
        capsuleObject.localScale = new Vector3(2*xRayRadius, raydistance/2, 2 * xRayRadius);

        foreach (Collider col in cols)
        {
            XRayBlock tb = col.gameObject.GetComponent<XRayBlock>();
            if (tb != null)
            {
                tb.MakeShadowOnly();
            }
        }

    }
}

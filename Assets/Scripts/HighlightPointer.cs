using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightPointer : MonoBehaviour {

    public bool checkForHighlightable;
    public LayerMask highlaytableLayers;
    public float distance;
    public float radious;
    public bool isHighlighting { get; private set; }
    public HighlightableObject highlightedObject { get; private set; }


    void Update()
    {
        RaycastForHighlightable();
    }

    void RaycastForHighlightable()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.CapsuleCast(ray.origin, ray.origin + ray.direction * distance, radious, ray.direction * distance,out hitInfo, distance, highlaytableLayers))
        {
            highlightedObject = hitInfo.collider.gameObject.GetComponentInParent<HighlightableObject>();
            if (highlightedObject != null)
            {
                isHighlighting = true;
                return;
            }

        }

        isHighlighting = false;
        highlightedObject = null;
    }

    private void OnDrawGizmos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Gizmos.DrawSphere(ray.origin, radious);
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * distance);
        
    }

}

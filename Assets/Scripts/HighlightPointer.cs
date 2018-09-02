using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightPointer : MonoBehaviour {

    public bool checkForHighlightable;
    public LayerMask highlaytableLayers;
    public float distance;
    public float radius;
    public bool isHighlighting { get; private set; }
    public HighlightableObject highlightedObject { get; private set; }
    

    void Update()
    {
        RaycastForHighlightable();
    }

    void RaycastForHighlightable()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hitInfo = Physics.CapsuleCastAll(ray.origin, ray.origin + ray.direction * distance, radius, ray.direction, 0, highlaytableLayers);

        foreach (RaycastHit hit in hitInfo)
        {
            highlightedObject = hit.collider.gameObject.GetComponentInParent<HighlightableObject>();
            if (highlightedObject != null)
            {

                if (!isHighlighting)
                {
                    HighlightOn();
                }
                
                return;
            }
        }


        HighlightOff();
    }

    private void OnDrawGizmos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Gizmos.DrawSphere(ray.origin, radius);
        Gizmos.DrawSphere(ray.origin + ray.direction * distance, radius);
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * distance);

    }

    private void HighlightOn()
    {
        isHighlighting = true;
    }

    private void HighlightOff()
    {
        isHighlighting = false;
        highlightedObject = null;
    }


}

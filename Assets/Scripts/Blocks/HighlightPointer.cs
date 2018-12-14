using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightPointer : Singleton<HighlightPointer>
{
    //Variables
    public bool checkForHighlightable;
    public LayerMask highlaytableLayers;
    public float distance;
    public float radius;
    public bool isHighlighting { get; private set; }
    public HighlightableObject highlightedObject { get; private set; }
    public Player playerTeam {get{return LevelController.Instance.team;}}
    private LNetworkPlayer networkPlayer;

    public void clearHighlight(HighlightableObject objectRef)
    {
        if (objectRef == highlightedObject) HighlightOff();
    }

    void Update()
    {
        RaycastForHighlightable();
    }

    void RaycastForHighlightable()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            HighlightOff();
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Collider[] colliders = Physics.OverlapCapsule(ray.origin, ray.origin + ray.direction * distance, radius, highlaytableLayers, QueryTriggerInteraction.Collide);

        float distanceFromRay = float.MaxValue;
        bool highlighted = false;

        foreach (Collider col in colliders)
        {
            HighlightableObject highlightScript = col.gameObject.GetComponentInParent<HighlightableObject>();
            if (highlightScript != null && highlightScript.canBeHighlighted)
            {
                LemmingStateController lemming = highlightScript.GetComponent<LemmingStateController>();
                if (lemming != null)
                {
                    if (lemming.Team != playerTeam)
                    {
                        continue;
                    }
                }

                float angle = Vector3.Angle(ray.direction, (col.bounds.center - Camera.main.transform.position));
                float dist = Mathf.Sin(Mathf.Deg2Rad * angle) * Vector3.Distance(col.bounds.center, Camera.main.transform.position);
                if (dist < distanceFromRay)
                {
                    distanceFromRay = distance;
                    highlightedObject = highlightScript;
                    highlighted = true;
                }
            }
        }

        if (highlighted && !isHighlighting)
        {
            HighlightOn();
        }
        else if (!highlighted && isHighlighting)
        {
            HighlightOff();
        }
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

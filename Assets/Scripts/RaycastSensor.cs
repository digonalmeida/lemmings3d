using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RaycastSensor
{
    [SerializeField]
    private Vector3 origin = Vector3.zero;

    [SerializeField]
    private Vector3 direction = Vector3.right;

    [SerializeField]
    private float distance = 1;

    [SerializeField]
    private LayerMask layerMask;

    private bool isTriggered = false;
    public bool IsTriggered
    {
        get
        {
            return isTriggered;
        }
    }

    private Ray GetLocalRay(Transform agent)
    {
        var ray = new Ray();
        ray.origin = agent.position + (Vector3)(agent.localToWorldMatrix * origin);
        ray.direction = agent.localToWorldMatrix * direction;
        ray.direction *= distance;
        return ray;
    }

    public void DrawGizmos(Transform agent)
    {
        Check(agent);
        Gizmos.color = isTriggered ? Color.green : Color.red;
        var ray = GetLocalRay(agent);
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction);
    }
    
    public bool Check(Transform agent)
    {
        var ray = GetLocalRay(agent);
        isTriggered = Physics.Raycast(ray, distance, layerMask);
        return isTriggered;
    }
}

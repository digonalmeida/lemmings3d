using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceDirection : MonoBehaviour {
    [SerializeField]
    private Vector3 direction = Vector3.right;

    void OnTriggerEnter(Collider other)
    {
        var lemming = other.GetComponent<LemmingController>();
        if(lemming == null)
        {
            return;
        }
        lemming.transform.forward = direction;
    }
}

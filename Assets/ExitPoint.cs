using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPoint : MonoBehaviour {

    public Transform exitLemmingFinalTransform;

    public Vector3 direction
    {
        get
        {
            return transform.forward;
        }
    }

    /*
    void OnTriggerEnter(Collider other) {

        if (other.gameObject.layer == LayerMask.NameToLayer("Lemming"))
        {
            LemmingMovementController lmc = other.GetComponent<LemmingMovementController>();

            LemmingActions la = other.GetComponent<LemmingActions>();
            if (la != null)
            {
                la.EnterExitPoint();
            }
        }

    }
    */

}

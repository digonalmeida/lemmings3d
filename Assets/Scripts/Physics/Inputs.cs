using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    //References
    public HighlightPointer highlightPointerRef;

    // Update is called once per frame
    void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            if (highlightPointerRef.isHighlighting)
            {
                LemmingStateController lemming = highlightPointerRef.highlightedObject.center.GetComponent<LemmingStateController>();
                ControllerManager.Instance.skillController.assignSkill(lemming);
            }
        }
	}
}

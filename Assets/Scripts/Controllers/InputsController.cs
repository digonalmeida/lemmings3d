using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsController : MonoBehaviour
{
    //References
    public HighlightPointer highlightPointerRef;

    // Update is called once per frame
    void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            if (!SkillsController.Instance.isWaitingForBlockerConfirmation)
            {
                if (highlightPointerRef.isHighlighting)
                {
                    LemmingStateController lemming = highlightPointerRef.highlightedObject.center.GetComponent<LemmingStateController>();
                    SkillsController.Instance.assignSkill(lemming);
                }
            }
        }
	}
}

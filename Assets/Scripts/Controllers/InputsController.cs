using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsController : MonoBehaviour
{
    //References
    public HighlightPointer highlightPointerRef;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // if has any selected skill
            if (SkillsController.Instance.selectedSkill != Skill.None)
            {
                if (SkillsController.Instance.isWaitingForBlockerConfirmation)
                {
                    if (!SkillsController.Instance.blockerSelector.someButtonHighlighted)
                    {
                        //Cancel Directioner
                        SkillsController.Instance.cancelSkill();
                        return;
                    }
                }
                else
                {
                    if (!highlightPointerRef.isHighlighting)
                    {
                        SkillsController.Instance.cancelSkill();
                        return;
                    }
                }

                if (!SkillsController.Instance.isWaitingForBlockerConfirmation)
                {
                    if (highlightPointerRef.isHighlighting)
                    {
                        LemmingStateController lemming = highlightPointerRef.highlightedObject.GetComponent<LemmingStateController>();
                        SkillsController.Instance.assignSkill(lemming);
                    }
                }
            }
        }
    }
}

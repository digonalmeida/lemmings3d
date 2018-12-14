using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsController : MonoBehaviour
{
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
                    if (!HighlightPointer.Instance.isHighlighting)
                    {
                        SkillsController.Instance.cancelSkill();
                        return;
                    }
                }

                if (!SkillsController.Instance.isWaitingForBlockerConfirmation)
                {
                    if (HighlightPointer.Instance.isHighlighting)
                    {
                        LemmingStateController lemming = HighlightPointer.Instance.highlightedObject.GetComponent<LemmingStateController>();
                        SkillsController.Instance.assignSkill(lemming);
                    }
                }
            }
        }
    }
}

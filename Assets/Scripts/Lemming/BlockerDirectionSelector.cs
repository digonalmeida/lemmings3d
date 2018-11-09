using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerDirectionSelector : MonoBehaviour {

    private LemmingStateController followingLemming;
    private float cameraScale;
    private bool visible;
    public GameObject parentPanel;

    public void AttachToSomeone(LemmingStateController following)
    {
        followingLemming = following;
        Show();
    }

    public void Detach()
    {
        followingLemming = null;
        Hide();
    }

    private void Start()
    {
        cameraScale = transform.localScale.x / Vector3.Distance(Camera.main.transform.position,transform.position);

        Hide();
    }


    void LateUpdate () {

        if (visible)
        {
            if(followingLemming != null) transform.position = followingLemming.transform.position;
            transform.localScale = Vector3.one * cameraScale * Vector3.Distance(Camera.main.transform.position, transform.position);
        }

    }

    public void ClickDirection(int direction)
    {
        if (visible)
        {
            switch (direction)
            {
                case 0:
                    ControllerManager.Instance.skillController.selectedSkill = Skill.Blocker_TurnEast;
                    break;
                case 1:
                    ControllerManager.Instance.skillController.selectedSkill = Skill.Blocker_TurnNorth;
                    break;
                case 2:
                    ControllerManager.Instance.skillController.selectedSkill = Skill.Blocker_TurnWest;
                    break;
                case 3:
                    ControllerManager.Instance.skillController.selectedSkill = Skill.Blocker_TurnSouth; 
                    break;
                default:
                    break;
            }

            ControllerManager.Instance.skillController.assignSkill(followingLemming);

            Detach();

        }

    }


    private void Show()
    {
        parentPanel.SetActive(true);
        visible = true;
    }

    private void Hide()
    {
        parentPanel.SetActive(false);
        visible = false;
    }

}

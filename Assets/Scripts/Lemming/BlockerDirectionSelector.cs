using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BlockerDirectionSelector : MonoBehaviour
{

    private LemmingStateController followingLemming;
    private float cameraScale = 1;
    private bool visible = false;
    public GameObject parentPanel;

    private DirectionerButton[] buttons;
    public bool someButtonHighlighted { get; private set; }

    void Awake()
    {
        buttons = GetComponentsInChildren<DirectionerButton>();
    }

    void Update()
    {
        if (visible)
        {
            someButtonHighlighted = buttons.Where(i => i.isHighlighted).ToArray().Length > 0;
        }
    }

    void LateUpdate()
    {

        if (visible)
        {
            if (followingLemming != null) transform.position = followingLemming.transform.position;
            transform.localScale = Vector3.one * cameraScale * Vector3.Distance(Camera.main.transform.position, transform.position);
        }

    }

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
        cameraScale = transform.localScale.x / Vector3.Distance(Camera.main.transform.position, transform.position);
        Hide();
    }



    public void ClickDirection(int direction)
    {
        if (visible)
        {
            switch (direction)
            {
                case 0:
                    SkillsController.Instance.selectedSkill = Skill.Blocker_TurnEast;
                    break;
                case 1:
                    SkillsController.Instance.selectedSkill = Skill.Blocker_TurnNorth;
                    break;
                case 2:
                    SkillsController.Instance.selectedSkill = Skill.Blocker_TurnWest;
                    break;
                case 3:
                    SkillsController.Instance.selectedSkill = Skill.Blocker_TurnSouth;
                    break;
                default:
                    break;
            }

            SkillsController.Instance.assignSkill(followingLemming);

            Detach();

        }

    }


    private void Show()
    {
        parentPanel.SetActive(true);
        visible = true;
        someButtonHighlighted = false;
    }

    private void Hide()
    {
        parentPanel.SetActive(false);
        visible = false;
        someButtonHighlighted = false;
    }

}

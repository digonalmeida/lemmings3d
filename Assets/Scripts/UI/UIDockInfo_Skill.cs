using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDockInfo_Skill : UIDockInfo
{
    //References
    public Toggle toggleButton;
    public Image togglePanelRef;

    //Control Variables
    private int lastCount;
    public Skill skill;
    public Text skillCountText;

    private void OnEnable()
    {
        LevelController.LemmingUsedSkill += UpdateInfo;
    }

    private void OnDisable()
    {
        LevelController.LemmingUsedSkill -= UpdateInfo;
    }

    //Start Method
    private void Start()
    {
        lastCount = ControllerManager.Instance.skillController.getRemainingUses(skill);
        UpdateInfo();
    }

    //Update Control Variables when Toggle Value Changes
    public void updateState(bool active)
    {
        if (active)
        {
            togglePanelRef.color = Color.red;
            ControllerManager.Instance.skillController.changeSkill(skill);
        }
        else
        {
            togglePanelRef.color = Color.white;
            ControllerManager.Instance.skillController.selectedSkill = Skill.None;
        }
    }

    //Reset Toggle to Off State
    private void resetToggle()
    {
        toggleButton.isOn = false;
    }

    //Update Display Variables
    private void UpdateInfo()
    {
        int skillCount = ControllerManager.Instance.skillController.getRemainingUses(skill);
        if (lastCount > skillCount)
        {
            resetToggle();
            lastCount = skillCount;
        }

        skillCountText.text = skillCount.ToString();
        toggleButton.interactable = skillCount > 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDockInfo_Skill : UIDockInfo
{
    //References
    public Toggle toggleButton;
    public Image togglePanelRef;
    private SkillsController skillsControllerRef;

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
        skillsControllerRef = SkillsController.Instance;
        lastCount = skillsControllerRef.getRemainingUses(skill);
        UpdateInfo();
    }

    //Update Control Variables when Toggle Value Changes
    public void updateState(bool active)
    {
        if (active)
        {
            togglePanelRef.color = Color.red;
            skillsControllerRef.selectedSkill = skill;
        }
        else
        {
            togglePanelRef.color = Color.white;
            skillsControllerRef.selectedSkill = Skill.None;
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
        int skillCount = skillsControllerRef.getRemainingUses(skill);
        if (lastCount > skillCount)
        {
            resetToggle();
            lastCount = skillCount;
        }

        skillCountText.text = skillCount.ToString();
        toggleButton.interactable = skillCount > 0;
    }
}

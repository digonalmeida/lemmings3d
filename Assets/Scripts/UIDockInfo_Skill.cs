using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDockInfo_Skill : UIDockInfo
{
    //References
    public Toggle toggleButton;
    private SkillsController skillsControllerRef;
    private Image togglePanelRef;

    //Control Variables
    private int lastCount;
    public Skill skill;
    public Text skillCountText;
    
    //Start Method
    private void Start()
    {
        skillsControllerRef = SkillsController.Instance;
        togglePanelRef = toggleButton.transform.parent.GetComponent<Image>();
        lastCount = skillsControllerRef.getRemainingUses(skill);
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
    public override void UpdateInfo()
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDockInfo_Skill : UIDockInfo
{
    //References
    private SkillsController skillsControllerRef;
    public Skill skill;
    public Text skillCountText;
    public Toggle toggleButton;

    private void Start()
    {
        skillsControllerRef = SkillsController.Instance;
    }

    public void Activate()
    {
        skillsControllerRef.selectedSkill = skill;
    }

    public override void UpdateInfo()
    {
        int skillCount = skillsControllerRef.getRemainingUses(skill);
        skillCountText.text = skillCount.ToString();
        toggleButton.interactable = skillCount > 0;
    }
}

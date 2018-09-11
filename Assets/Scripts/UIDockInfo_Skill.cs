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
    public Button skillButton;

    //Control Variables
    public bool skillEnabled;

    private void Start()
    {
        skillsControllerRef = SkillsController.Instance;
    }

    public override void UpdateInfo()
    {
        int skillCount = skillsControllerRef.getRemainingUses(skill);
        skillCountText.text = skillCount.ToString();
        skillButton.interactable = !(!skillEnabled || skillCount <= 0);
    }
}

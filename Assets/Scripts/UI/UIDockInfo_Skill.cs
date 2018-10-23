using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDockInfo_Skill : UIDockInfo
{
    //References
    public Toggle toggleButton;
    private Image togglePanelRef;

    //Control Variables
    public Skill skill;
    public Text skillCountText;

    private void OnEnable()
    {
        LevelController.LemmingUsedSkill += UpdateInfo;
        LevelController.OnLoadGame += UpdateInfo;
    }

    private void OnDisable()
    {
        LevelController.LemmingUsedSkill -= UpdateInfo;
        LevelController.OnLoadGame -= UpdateInfo;
    }

    //Start Method
    private void Start()
    {
        togglePanelRef = GetComponent<Image>();
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
            toggleButton.isOn = false;
        }
    }

    //Update Display Variables
    private void UpdateInfo()
    {
        updateState(ControllerManager.Instance.skillController.selectedSkill != Skill.None);
        int skillCount = ControllerManager.Instance.skillController.getRemainingUses(skill);
        skillCountText.text = skillCount.ToString();
        toggleButton.interactable = skillCount > 0;
    }
}

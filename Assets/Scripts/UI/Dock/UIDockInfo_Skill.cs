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
        GameEvents.Lemmings.LemmingUsedSkill += UpdateInfo;
        GameEvents.GameState.OnLoadGame += UpdateInfo;
    }

    private void OnDisable()
    {
        GameEvents.Lemmings.LemmingUsedSkill -= UpdateInfo;
        GameEvents.GameState.OnLoadGame -= UpdateInfo;
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
            SkillsController.Instance.changeSkill(skill);
            GameEvents.UI.SelectedSkill.SafeInvoke();
        }
        else
        {
//            togglePanelRef.color = Color.white;
            SkillsController.Instance.selectedSkill = Skill.None;
            toggleButton.isOn = false;
        }
    }

    //Update Display Variables
    private void UpdateInfo()
    {

        updateState(SkillsController.Instance.selectedSkill != Skill.None);
        int skillCount = SkillsController.Instance.getRemainingUses(skill);
        skillCountText.text = skillCount.ToString();
        toggleButton.interactable = skillCount > 0;
    }
}

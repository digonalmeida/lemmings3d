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
        GameEvents.UI.OnSkillsLoaded += OnSkillsLoaded;
    }

    private void OnDisable()
    {
        GameEvents.Lemmings.LemmingUsedSkill -= UpdateInfo;
        GameEvents.UI.OnSkillsLoaded -= OnSkillsLoaded;
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
            togglePanelRef.color = Color.white;
            SkillsController.Instance.selectedSkill = Skill.None;
            toggleButton.isOn = false;
        }
    }

    //Update Display Variables
    private void UpdateInfo(LemmingStateController lemming = null)
    {
        updateState(SkillsController.Instance.selectedSkill != Skill.None);
        int skillCount = SkillsController.Instance.getRemainingUses(skill);
        skillCountText.text = skillCount.ToString();
        toggleButton.interactable = skillCount > 0;
    }

    private void OnSkillsLoaded(){
        UpdateInfo(null);
    }
}

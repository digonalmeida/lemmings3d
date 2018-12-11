using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIDock : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    public Animator animator;

    void OnEnable()
    {
        GameEvents.UI.DeselectedSkill += SwitchAllOff;
        GameEvents.UI.OpenInGameUI += Open;
        GameEvents.UI.CloseInGameUI += Close;
    }

    void OnDisable()
    {
        GameEvents.UI.DeselectedSkill -= SwitchAllOff;
        GameEvents.UI.OpenInGameUI -= Open;
        GameEvents.UI.CloseInGameUI -= Close;
    }


    public void ExplodeAllButton()
    {
        SkillsController.Instance.explodeAll();
    }

    public void DebugText(string text)
    {
        Debug.Log(text);
    }

    public void SwitchAllOff()
    {
        toggleGroup.SetAllTogglesOff();
    }

    void Open()
    {
        animator.SetBool("opened", true);
    }

    void Close()
    {
        animator.SetBool("opened", false);
    }
}

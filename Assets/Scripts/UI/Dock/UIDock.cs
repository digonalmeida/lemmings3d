using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIDock : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    public UnityEvent pause;
    public UnityEvent explodeAll;

    void OnEnable(){
        GameEvents.UI.DeselectedSkill += SwitchAllOff;
    }

    void OnDisable(){
        GameEvents.UI.DeselectedSkill -= SwitchAllOff;
    }

    public void PauseButton()
    {
        pause.Invoke();
    }

    public void ExplodeAllButton()
    {
        explodeAll.Invoke();
    }

    public void DebugText(string text)
    {
        Debug.Log(text);
    }

    public void SwitchAllOff()
    {
        toggleGroup.SetAllTogglesOff();
    }

}

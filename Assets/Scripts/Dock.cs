using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dock : MonoBehaviour {

    public UnityEvent pause;
    public UnityEvent explodeAll;
    public UnityEvent decreaseRate;
    public UnityEvent increaseRate;
	public UnityEvent turnSkill;
    public UnityEvent parachuteSkill;

    public void PauseButton()
    {
        pause.Invoke();
    }

    public void ExplodeAllButton()
    {
        explodeAll.Invoke();
    }

    public void DecreaseRateButton()
    {
        decreaseRate.Invoke();
    }

    public void IncreaseRateButton()
    {
        increaseRate.Invoke();
    }

    public void TurnSkillButton()
    {
        turnSkill.Invoke();
    }

    public void ParachuteSkillButton()
    {
        parachuteSkill.Invoke();
    }


    private void Update()
    {

        UpdateDockInfo();

    }

    private void UpdateDockInfo()
    {
        return;
    }
}

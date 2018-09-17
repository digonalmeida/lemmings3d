using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIDock : MonoBehaviour
{
    public List<UIDockInfo> dockInfos = new List<UIDockInfo>();

    public UnityEvent pause;
    public UnityEvent explodeAll;
    public UnityEvent decreaseRate;
    public UnityEvent increaseRate;

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

    public void DebugText(string text)
    {
        Debug.Log(text);
    }

    private void Update()
    {
        UpdateDockInfo();
    }

    private void UpdateDockInfo()
    {
        foreach (var info in dockInfos)
        {
            info.UpdateInfo();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDockInfo_Timer : UIDockInfo
{

    public Text timeText;
    public float time;

    //TODO
    private void Update()
    {
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        if (LevelController.Instance.gameStateManager == null) return;

        time = LevelController.Instance.remainingTime;
        timeText.text = ((int)time / 60).ToString("0") + ":" + (time % 60).ToString("00");

    }
}

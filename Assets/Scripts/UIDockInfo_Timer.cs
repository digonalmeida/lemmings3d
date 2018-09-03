using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDockInfo_Timer : UIDockInfo {

    public Text timeText;
    public float time;

    public override void UpdateInfo()
    {
        time = Time.time;
        timeText.text = ((int)time / 60).ToString("0") + ":" + (time % 60).ToString("00");
    }

}

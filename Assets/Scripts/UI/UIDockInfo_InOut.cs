using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDockInfo_InOut : UIDockInfo
{

    public Text inText;
    public Text outText;
    public int inCount;
    public int outCount;


    private void OnEnable()
    {
        GameEvents.Lemmings.LemmingReachedExit += UpdateInfo;
        GameEvents.Lemmings.LemmingSpawned += UpdateInfo;
    }

    private void OnDisable()
    {
        GameEvents.Lemmings.LemmingReachedExit -= UpdateInfo;
        GameEvents.Lemmings.LemmingSpawned -= UpdateInfo;
    }


    private void UpdateInfo()
    {

        outCount = LevelController.lemmingsSpawned;
        inCount = LevelController.lemmingsEnteredExit;

        outText.text = outCount.ToString();
        inText.text = outCount == 0 ? "0%" : (((int)(((float)inCount / (float)outCount) * 100)).ToString() + "%");
    }
}

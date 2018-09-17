using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDockInfo_InOut : UIDockInfo
{
    private LevelController levelController;

    public Text inText;
    public Text outText;
    public int inCount;
    public int outCount;

    private void Awake()
    {
        levelController = GameObject.FindObjectOfType<LevelController>();
    }

    private void OnEnable()
    {
        LevelController.LemmingReachedExit += UpdateInfos;
        LevelController.LemmingSpawned += UpdateInfos;
    }

    private void OnDisable()
    {
        LevelController.LemmingReachedExit -= UpdateInfos;
        LevelController.LemmingSpawned -= UpdateInfos;
    }

    public override void UpdateInfo()
    {
        return;
    }

    public void UpdateInfos()
    {

        outCount = levelController.lemmingsSpawned;
        inCount = levelController.lemmingsEnteredExit;

        outText.text = outCount.ToString();
        inText.text = outCount == 0 ? "0%" : (((int)(((float)inCount / (float)outCount) * 100)).ToString() + "%");
    }
}

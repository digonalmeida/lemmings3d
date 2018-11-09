using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDockInfo_Speed : UIDockInfo
{

    public Text rateText;
    public Button minusSign;
    public Button plusSign;

    private void OnEnable()
    {
        GameEvents.Lemmings.ChangedSpawnRate += UpdateInfo;
    }

    private void OnDisable()
    {
        GameEvents.Lemmings.ChangedSpawnRate -= UpdateInfo;
    }


    private void Start()
    {
        UpdateInfo(LevelController.currentRate);
    }

    public void IncreaseRate()
    {
        LevelController.ChangeSpawnRate(1);
    }

    public void DecreaseRate()
    {
        LevelController.ChangeSpawnRate(-1);
    }

    private void UpdateInfo(int newSpawnRate)
    {
        rateText.text = newSpawnRate.ToString();

        minusSign.interactable = LevelController.currentRate > LevelController.minimumSpawnRate;
        plusSign.interactable = LevelController.currentRate < LevelController.maximumSpawnRate;
    }
}

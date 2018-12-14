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
        GameEvents.GameState.OnLoadGame += UpdateInfo;
        GameEvents.GameState.OnStartGame += UpdateInfo;
    }

    private void OnDisable()
    {
        GameEvents.Lemmings.ChangedSpawnRate -= UpdateInfo;
        GameEvents.GameState.OnLoadGame -= UpdateInfo;
        GameEvents.GameState.OnStartGame -= UpdateInfo;
    }


    public void IncreaseRate()
    {
        LevelController.Instance.ChangeSpawnRate(1);
    }

    public void DecreaseRate()
    {
        LevelController.Instance.ChangeSpawnRate(-1);
    }

    private void UpdateInfo()
    {
        int newSpawnRateIndex = LevelController.Instance.currentSpawnRateIndex;

        rateText.text = newSpawnRateIndex.ToString();

        minusSign.interactable = LevelController.Instance.currentSpawnRateIndex > 0;
        plusSign.interactable = LevelController.Instance.currentSpawnRateIndex < LevelController.Instance.spawnLemmingsPerSecondRates.Count - 1;
    }

    private void UpdateInfo(Player team)
    {
        UpdateInfo();
    }
}

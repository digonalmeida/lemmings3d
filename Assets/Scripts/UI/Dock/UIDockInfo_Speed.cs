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
    }

    private void OnDisable()
    {
        GameEvents.Lemmings.ChangedSpawnRate -= UpdateInfo;
        GameEvents.GameState.OnLoadGame -= UpdateInfo;
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
        int newSpawnRate = LevelController.Instance.currentSpawnRate;

        rateText.text = newSpawnRate.ToString();

        minusSign.interactable = LevelController.Instance.currentSpawnRate > LevelController.Instance.CurrentMapSettings.MinimumSpawnRate;
        plusSign.interactable = LevelController.Instance.currentSpawnRate < LevelController.Instance.CurrentMapSettings.MaximumSpawnRate;
    }
}

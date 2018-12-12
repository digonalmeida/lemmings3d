using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUiController : MonoBehaviour
{
    private void Awake()
    {
        GameEvents.GameState.OnLoadGame += TriggerOpenIngameUI;
        GameEvents.GameState.OnEndGame += TriggerCloseIngameUI;
        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEvents.GameState.OnLoadGame -= TriggerOpenIngameUI;
        GameEvents.GameState.OnEndGame -= TriggerCloseIngameUI;
    }

    void TriggerOpenIngameUI()
    {
        GameEvents.UI.OpenInGameUI.SafeInvoke();
        this.gameObject.SetActive(true);
    }

    void TriggerCloseIngameUI()
    {
        GameEvents.UI.CloseInGameUI.SafeInvoke();
        this.gameObject.SetActive(true);
    }
}

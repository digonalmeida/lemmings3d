using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUiController : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.GameState.OnLoadGame += TriggerOpenIngameUI;
        GameEvents.GameState.OnEndGame += TriggerCloseIngameUI;
    }

    private void OnDisable()
    {
        GameEvents.GameState.OnLoadGame -= TriggerOpenIngameUI;
        GameEvents.GameState.OnEndGame -= TriggerCloseIngameUI;
    }

    void TriggerOpenIngameUI()
    {
        GameEvents.UI.OpenInGameUI.SafeInvoke();
    }

    void TriggerCloseIngameUI()
    {
        GameEvents.UI.CloseInGameUI.SafeInvoke();
    }
}

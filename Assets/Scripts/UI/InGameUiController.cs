using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUiController : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.GameState.OnLoadGame += TriggerOpenIngameUI;
        GameEvents.GameState.OnEndGame += TriggerCloseIngameUI;
        this.GetComponent<UIHighlightObject>().enabled = false;
    }

    private void OnDisable()
    {
        GameEvents.GameState.OnLoadGame -= TriggerOpenIngameUI;
        GameEvents.GameState.OnEndGame -= TriggerCloseIngameUI;
    }

    void TriggerOpenIngameUI()
    {
        GameEvents.UI.OpenInGameUI.SafeInvoke();
        this.GetComponent<UIHighlightObject>().enabled = true;
    }

    void TriggerCloseIngameUI()
    {
        GameEvents.UI.CloseInGameUI.SafeInvoke();
        this.GetComponent<UIHighlightObject>().enabled = false;
    }
}

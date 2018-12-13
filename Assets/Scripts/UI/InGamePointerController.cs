using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePointerController : MonoBehaviour
{
    private void Awake()
    {
        GameEvents.UI.OpenInGameUI += TriggerOpenIngameUI;
        GameEvents.UI.CloseInGameUI += TriggerCloseIngameUI;
        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEvents.UI.OpenInGameUI -= TriggerOpenIngameUI;
        GameEvents.UI.CloseInGameUI -= TriggerCloseIngameUI;
    }

    void TriggerOpenIngameUI()
    {
        this.gameObject.SetActive(true);
    }

    void TriggerCloseIngameUI()
    {
        this.gameObject.SetActive(false);
    }
}

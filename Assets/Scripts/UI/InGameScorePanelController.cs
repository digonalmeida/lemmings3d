using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScorePanelController : MonoBehaviour
{
    private void Awake()
    {
        GameEvents.GameState.OnEndGame += TriggerOpenScorePanel;
        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEvents.GameState.OnEndGame -= TriggerOpenScorePanel;
    }

    void TriggerOpenScorePanel()
    {
        this.gameObject.SetActive(true);
    }
}

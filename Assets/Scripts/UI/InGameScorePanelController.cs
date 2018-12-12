using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScorePanelController : MonoBehaviour
{
    private void Awake()
    {
        GameEvents.UI.OpenScorePanel += TriggerOpenScorePanel;
        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEvents.UI.OpenScorePanel -= TriggerOpenScorePanel;
    }

    void TriggerOpenScorePanel()
    {
        this.gameObject.SetActive(true);
    }
}

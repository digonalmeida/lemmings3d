using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanelController : MonoBehaviour
{
    //Variables
    public GameObject rematchButton;

    //Set Rematch Ready
    public void setPlayerReadyForRematch()
    {
        if (LNetworkPlayer.LocalInstance != null)
        {
            LNetworkPlayer.LocalInstance.CmdInformRematchReady(true);
            rematchButton.GetComponent<Image>().color = Color.yellow;
            rematchButton.transform.GetChild(0).GetComponent<Text>().text = "Waiting for Opponent";
        }
    }
}

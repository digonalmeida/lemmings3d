using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class HostGameButton : MonoBehaviour
{
    //References
    public PanelFlow panelFlowScriptRef;
    private string roomName;

    //Update Room Name
    public void updateRoomName(string name)
    {
        roomName = name;
    }

    //Create Lan Match
    public void CreateMatch()
    {
        if (!string.IsNullOrEmpty(roomName))
        {
            LNetworkLobbyManager networkManager = LNetworkLobbyManager.singleton.GetComponent<LNetworkLobbyManager>();
            networkManager.matchMaker.CreateMatch(roomName, 2, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
            panelFlowScriptRef.changePanel(5);
        }
    }
}

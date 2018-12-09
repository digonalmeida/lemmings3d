using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LNetworkLobbyManager : NetworkLobbyManager 
{
    //References
    public GameObject disconnectedPanel;

    public override void OnLobbyClientDisconnect(NetworkConnection conn)
    {
        base.OnLobbyClientDisconnect(conn);
        Instantiate(disconnectedPanel);
    }

    public override void OnLobbyServerDisconnect(NetworkConnection conn)
    {
        base.OnLobbyServerDisconnect(conn);
        if(client != null) LobbyPanelManager.Instance.resetPlayer2();
    }
}

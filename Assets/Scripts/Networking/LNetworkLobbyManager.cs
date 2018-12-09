using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LNetworkLobbyManager : NetworkLobbyManager 
{
    //References
    public GameObject disconnectedPanel;
    public GameObject opponentLeftPanel;

    public override void OnLobbyClientDisconnect(NetworkConnection conn)
    {
        base.OnLobbyClientDisconnect(conn);
        Instantiate(disconnectedPanel);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnLobbyClientDisconnect(conn);
        Instantiate(disconnectedPanel);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        Instantiate(opponentLeftPanel);
    }

    public override void OnLobbyServerDisconnect(NetworkConnection conn)
    {
        base.OnLobbyServerDisconnect(conn);
        if(client != null) LobbyPanelManager.Instance.resetPlayer2();
    }
}

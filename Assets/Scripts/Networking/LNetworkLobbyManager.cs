using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LNetworkLobbyManager : NetworkLobbyManager 
{
    public override void OnLobbyServerDisconnect(NetworkConnection conn)
    {
        base.OnLobbyServerDisconnect(conn);
        LobbyPanelManager.Instance.resetPlayer2();
    }
}

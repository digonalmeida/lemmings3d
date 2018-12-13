using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LNetworkLobbyManager : NetworkLobbyManager 
{
    //References
    public GameObject disconnectedPanel;
    public GameObject opponentLeftPanel;

    public override void OnStopClient()
    {
        base.OnStopClient();
        if (NetworkServer.active) StopServer();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnLobbyClientDisconnect(conn);
        SceneManager.LoadScene("Menu");
        Instantiate(disconnectedPanel);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        if (SceneManager.GetActiveScene().name == "DefaultLevel")
        {
            StopClient();
            SceneManager.LoadScene("Menu");
            Instantiate(opponentLeftPanel);
        }
        else if(SceneManager.GetActiveScene().name == "Menu")
        {
            if (client != null) LobbyPanelManager.Instance.resetPlayer2();
        }
    }
}

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

    //Control Variables
    private bool startTimer = false;
    private bool stoppingMusic = false;
    private float currentTimer;
    public float countTimer = 3.5f;


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

    public override void OnLobbyServerPlayersReady()
    {
        startTimer = true;
        currentTimer = countTimer;
    }

    private void Update()
    {
        if (startTimer)
        {
            if (!stoppingMusic)
            {
                LNetworkLobbyPlayer.LocalInstance.RpcFadeMusic();
                stoppingMusic = true;
            }

            currentTimer -= Time.deltaTime;
            if(currentTimer <= 0)
            {
                startTimer = false;
                stoppingMusic = false;
                ServerChangeScene(playScene);
            }
        }
    }
}

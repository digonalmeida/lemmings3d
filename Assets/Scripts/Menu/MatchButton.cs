using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class MatchButton : MonoBehaviour 
{
	//Variables
    private NetworkBroadcastResult networkDataBroadcast;
    private MatchInfoSnapshot networkDataMatch;
    private LNetworkLobbyManager networkManager;

    private void Start() 
    {
        networkManager = LNetworkLobbyManager.singleton.GetComponent<LNetworkLobbyManager>();
    }

    //Update Info
    public void updateInfo(MatchInfoSnapshot networkData)
    {
        GetComponentInChildren<Text>().text = networkData.name;
        this.networkDataMatch = networkData;
    }

    //Connect
    public void connect()
    {
        networkManager.matchMaker.JoinMatch(networkDataMatch.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
        FindObjectOfType<PanelFlow>().loadLobby();
    }
}

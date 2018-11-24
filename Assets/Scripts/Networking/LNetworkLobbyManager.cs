using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LNetworkLobbyManager : NetworkLobbyManager 
{
    public int levelId = 0;

    public override void OnMatchJoined(bool success, string extendedInfo, UnityEngine.Networking.Match.MatchInfo matchInfo)
    {
        base.OnMatchJoined(success, extendedInfo, matchInfo);
        //GameManager.Instance.LoadLevelById(levelId);
        Debug.Log("here");
    }
}

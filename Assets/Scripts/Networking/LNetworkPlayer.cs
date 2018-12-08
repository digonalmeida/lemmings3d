using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LNetworkPlayer : NetworkBehaviour 
{
    //Variables
    public Player playerNum;

    //Start
    private void Start()
    {
        //Set Player
        if (NetworkServer.active) playerNum = Player.Player1;
        else playerNum = Player.Player2;
    }

    //Get Local Lobby Player
    public static LNetworkPlayer getLocalPlayer()
    {
        LNetworkPlayer[] list = FindObjectsOfType<LNetworkPlayer>();
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].hasAuthority) return list[i];
        }
        return null;
    }

    //Get Opponent Lobby Player
    public static LNetworkPlayer getOpponentPlayer()
    {
        LNetworkPlayer[] list = FindObjectsOfType<LNetworkPlayer>();
        for (int i = 0; i < list.Length; i++)
        {
            if (!list[i].hasAuthority) return list[i];
        }
        return null;
    }

    [Command]
    public void CmdSelectLevel(int indexButton)
    {
        LevelSelectorNetworkController.Instance.selectLevel(indexButton, playerNum);
    }

    [ClientRpc]
    public void RpcSelectLevel(int indexButtonPlayer1, int indexButtonPlayer2)
    {
        LevelSelectorLocalController.Instance.updateToggle(indexButtonPlayer1, indexButtonPlayer2, playerNum);
    }
}

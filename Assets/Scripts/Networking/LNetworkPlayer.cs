using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LNetworkPlayer : NetworkBehaviour 
{
    //Variables
    [SyncVar]
    public Player playerNum;
    [SyncVar]
    public bool levelSelectReady = false;

    //Start
    public override void OnStartAuthority()
    {
        //Base Method
        base.OnStartAuthority();

        //Set Player
        if (isServer) playerNum = Player.Player1;
        else playerNum = Player.Player2;

        //Inform Player Num
        CmdInformPlayerNum(playerNum);
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
    public void CmdInformReadyStatus(bool ready)
    {
        levelSelectReady = ready;
    }

    [Command]
    public void CmdInformPlayerNum(Player playerNum)
    {
        this.playerNum = playerNum;
    }

    [Command]
    public void CmdSelectLevel(int indexButton)
    {
        LevelSelectorNetworkController.Instance.selectLevel(indexButton, playerNum);
    }

    [ClientRpc]
    public void RpcSelectLevel(int indexSelection, Player playerNum)
    {
        LevelSelectorPanelController.Instance.selectLevel(indexSelection, playerNum);
    }
}

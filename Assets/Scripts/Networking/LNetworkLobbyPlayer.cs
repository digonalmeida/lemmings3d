using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LNetworkLobbyPlayer : NetworkLobbyPlayer
{
    //References
    private Material playerClothMaterialRef;
    private Material playerHairMaterialRef;
    private Material opponentClothMaterialRef;
    private Material opponentHairMaterialRef;
    private LNetworkLobbyPlayer opponentLobbyPlayer;

    //SyncVar Variables
    [SyncVar]
    public Player playerNum;
    [SyncVar]
    public string playerName;
    [SyncVar]
    public Color playerClothColor = Color.grey;
    [SyncVar]
    public Color playerHairColor = Color.grey;

    //On Start Authority
    public override void OnStartAuthority()
    {
        //Base Method
        base.OnStartAuthority();

        //Set Player
        if (NetworkServer.active) playerNum = Player.Player1;
        else playerNum = Player.Player2;

        //Set Initial References
        if (isServer)
        {
            LobbyPanelManager.Instance.setColorChangeButtons(Player.Player1, true);
            LobbyPanelManager.Instance.setColorChangeButtons(Player.Player2, false);
            playerClothMaterialRef = LobbyPanelManager.Instance.player1Lemming.GetComponentInChildren<SkinnedMeshRenderer>().materials[1];
            playerHairMaterialRef = LobbyPanelManager.Instance.player1Lemming.GetComponentInChildren<SkinnedMeshRenderer>().materials[2];
            opponentClothMaterialRef = LobbyPanelManager.Instance.player2Lemming.GetComponentInChildren<SkinnedMeshRenderer>().materials[1];
            opponentHairMaterialRef = LobbyPanelManager.Instance.player2Lemming.GetComponentInChildren<SkinnedMeshRenderer>().materials[2];
        }
        else
        {
            LobbyPanelManager.Instance.setColorChangeButtons(Player.Player2, true);
            LobbyPanelManager.Instance.setColorChangeButtons(Player.Player1, false);
            playerClothMaterialRef = LobbyPanelManager.Instance.player2Lemming.GetComponentInChildren<SkinnedMeshRenderer>().materials[1];
            playerHairMaterialRef = LobbyPanelManager.Instance.player2Lemming.GetComponentInChildren<SkinnedMeshRenderer>().materials[2];
            opponentClothMaterialRef = LobbyPanelManager.Instance.player1Lemming.GetComponentInChildren<SkinnedMeshRenderer>().materials[1];
            opponentHairMaterialRef = LobbyPanelManager.Instance.player1Lemming.GetComponentInChildren<SkinnedMeshRenderer>().materials[2];
        }

        //Finally...
        CmdRequestNextHairColor();
        CmdRequestNextClothColor();
        CmdInformPlayerName(UserData.name, playerNum);
    }

    //Get Player Num
    public Player getPlayerNum()
    {
        return playerNum;
    }

    //Get Local Lobby Player
    public static LNetworkLobbyPlayer getLocalLobbyPlayer()
    {
        LNetworkLobbyPlayer[] list = FindObjectsOfType<LNetworkLobbyPlayer>();
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].hasAuthority) return list[i];
        }
        return null;
    }

    //Get Opponent Lobby Player
    public static LNetworkLobbyPlayer getOpponentLobbyPlayer()
    {
        LNetworkLobbyPlayer[] list = FindObjectsOfType<LNetworkLobbyPlayer>();
        for (int i = 0; i < list.Length; i++)
        {
            if (!list[i].hasAuthority) return list[i];
        }
        return null;
    }

    //Get Opponent Lobby Player (by Saved Reference)
    private LNetworkLobbyPlayer getOpponentLobbyPlayerReference()
    {
        if (opponentLobbyPlayer == null) opponentLobbyPlayer = getOpponentLobbyPlayer();
        return opponentLobbyPlayer;
    }

    //Inform Name
    [Command]
    public void CmdInformPlayerName(string playerName, Player playerNum)
    {
        this.playerName = playerName;
        this.playerNum = playerNum;
        RpcUpdatePlayerName(playerName, playerNum);
    }

    //Request Color Changes
    [Command]
    public void CmdRequestPreviousHairColor()
    {
        playerHairColor = LemmingCustomizer.Instance.requestPreviousHairColor(playerNum, playerHairColor);
        RpcUpdateHairColor(playerHairColor);
    }

    [Command]
    public void CmdRequestNextHairColor()
    {
        playerHairColor = LemmingCustomizer.Instance.requestNextHairColor(playerNum, playerHairColor);
        RpcUpdateHairColor(playerHairColor);
    }

    [Command]
    public void CmdRequestPreviousClothColor()
    {
        playerClothColor = LemmingCustomizer.Instance.requestPreviousClothColor(playerNum, playerClothColor);
        RpcUpdateClothColor(playerClothColor);
    }

    [Command]
    public void CmdRequestNextClothColor()
    {
        playerClothColor = LemmingCustomizer.Instance.requestNextClothColor(playerNum, playerClothColor);
        RpcUpdateClothColor(playerClothColor);
    }

    [Command]
    public void CmdSetReadyOrUnready(bool ready)
    {
        RpcSetReadyOrUnready(playerNum, ready);
    }

    //Update Cloth Color
    [ClientRpc]
    public void RpcUpdateClothColor(Color newColor)
    {
        playerClothColor = newColor;
    }

    //Update Hair Color
    [ClientRpc]
    public void RpcUpdateHairColor(Color newColor)
    {
        playerHairColor = newColor;
    }

    //Update Player Name
    [ClientRpc]
    public void RpcUpdatePlayerName(string newName, Player setPlayerNum)
    {
        playerName = newName;
        playerNum = setPlayerNum;
    }

    [ClientRpc]
    public void RpcSetReadyOrUnready(Player playerNum, bool ready)
    {
        LobbyPanelManager.Instance.setPlayerReady(playerNum, ready);
    }

    //Update
    public void Update()
    {
        if(hasAuthority)
        {
            //Update Names
            if (playerNum == Player.Player1)
            {
                if (!playerName.Equals(LobbyPanelManager.Instance.player1Text.text)) LobbyPanelManager.Instance.player1Text.text = playerName;
                if (getOpponentLobbyPlayerReference() != null && !getOpponentLobbyPlayerReference().playerName.Equals(LobbyPanelManager.Instance.player2Text.text)) LobbyPanelManager.Instance.player2Text.text = getOpponentLobbyPlayerReference().playerName;
            }
            else if (playerNum == Player.Player2)
            {
                if (!playerName.Equals(LobbyPanelManager.Instance.player2Text.text)) LobbyPanelManager.Instance.player2Text.text = playerName;
                if (getOpponentLobbyPlayerReference() != null && !getOpponentLobbyPlayerReference().playerName.Equals(LobbyPanelManager.Instance.player1Text.text)) LobbyPanelManager.Instance.player1Text.text = getOpponentLobbyPlayerReference().playerName;
            }

            //Update Colors
            if (playerClothMaterialRef.color != playerClothColor) playerClothMaterialRef.color = playerClothColor;
            if (playerHairMaterialRef.color != playerHairColor) playerHairMaterialRef.color = playerHairColor;
            if (getOpponentLobbyPlayerReference() != null && opponentClothMaterialRef.color != getOpponentLobbyPlayerReference().playerClothColor) opponentClothMaterialRef.color = getOpponentLobbyPlayerReference().playerClothColor;
            if (getOpponentLobbyPlayerReference() != null && opponentHairMaterialRef.color != getOpponentLobbyPlayerReference().playerHairColor) opponentHairMaterialRef.color = getOpponentLobbyPlayerReference().playerHairColor;
        }
    }
}

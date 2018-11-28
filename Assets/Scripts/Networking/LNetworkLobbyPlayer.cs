using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LNetworkLobbyPlayer : NetworkLobbyPlayer
{
    //Control Variables
    public Player playerNum;

    //References
    private Material playerClothMaterialRef;
    private Material playerHairMaterialRef;
    private Material opponentClothMaterialRef;
    private Material opponentHairMaterialRef;

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
            LobbyPanelManager.Instance.setPlayerButtons(playerNum);
            playerClothMaterialRef = LobbyPanelManager.Instance.player1Lemming.GetComponentInChildren<SkinnedMeshRenderer>().materials[1];
            playerHairMaterialRef = LobbyPanelManager.Instance.player1Lemming.GetComponentInChildren<SkinnedMeshRenderer>().materials[2];
            opponentClothMaterialRef = LobbyPanelManager.Instance.player2Lemming.GetComponentInChildren<SkinnedMeshRenderer>().materials[1];
            opponentHairMaterialRef = LobbyPanelManager.Instance.player2Lemming.GetComponentInChildren<SkinnedMeshRenderer>().materials[2];
        }
        else
        {
            LobbyPanelManager.Instance.setPlayerButtons(playerNum);
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

    //Inform Name
    [Command]
    public void CmdInformPlayerName(string playerName, Player playerNum)
    {
        if (playerNum == Player.Player1) LemmingCustomizer.Instance.player1Name = playerName;
        else if (playerNum == Player.Player2) LemmingCustomizer.Instance.player2Name = playerName;
    }

    //Request Color Changes
    [Command]
    public void CmdRequestPreviousHairColor()
    {
        LemmingCustomizer.Instance.requestPreviousHairColor(playerNum);
    }

    [Command]
    public void CmdRequestNextHairColor()
    {
        LemmingCustomizer.Instance.requestNextHairColor(playerNum);
    }

    [Command]
    public void CmdRequestPreviousClothColor()
    {
        LemmingCustomizer.Instance.requestPreviousClothColor(playerNum);
    }

    [Command]
    public void CmdRequestNextClothColor()
    {
        LemmingCustomizer.Instance.requestNextClothColor(playerNum);
    }

    //Update
    private void FixedUpdate()
    {
        //Update Names
        if (LemmingCustomizer.Instance.player1Name != "" && !LobbyPanelManager.Instance.player1Text.text.Equals(LemmingCustomizer.Instance.player1Name)) LobbyPanelManager.Instance.player1Text.text = LemmingCustomizer.Instance.player1Name;
        if(LemmingCustomizer.Instance.player2Name != "" && !LobbyPanelManager.Instance.player2Text.text.Equals(LemmingCustomizer.Instance.player2Name)) LobbyPanelManager.Instance.player2Text.text = LemmingCustomizer.Instance.player2Name;

        //Update Materials
        if (playerNum == Player.Player1)
        {
            if(playerClothMaterialRef.color != LemmingCustomizer.Instance.player1ClothColor) playerClothMaterialRef.color = LemmingCustomizer.Instance.player1ClothColor;
            if (playerHairMaterialRef.color != LemmingCustomizer.Instance.player1HairColor) playerHairMaterialRef.color = LemmingCustomizer.Instance.player1HairColor;
            if (opponentClothMaterialRef.color != LemmingCustomizer.Instance.player2ClothColor) opponentClothMaterialRef.color = LemmingCustomizer.Instance.player2ClothColor;
            if (opponentHairMaterialRef.color != LemmingCustomizer.Instance.player2HairColor) opponentHairMaterialRef.color = LemmingCustomizer.Instance.player2HairColor;
        }
        else if (playerNum == Player.Player2)
        {
            if (playerClothMaterialRef.color != LemmingCustomizer.Instance.player2ClothColor) playerClothMaterialRef.color = LemmingCustomizer.Instance.player2ClothColor;
            if (playerHairMaterialRef.color != LemmingCustomizer.Instance.player2HairColor) playerHairMaterialRef.color = LemmingCustomizer.Instance.player2HairColor;
            if (opponentClothMaterialRef.color != LemmingCustomizer.Instance.player1ClothColor) opponentClothMaterialRef.color = LemmingCustomizer.Instance.player1ClothColor;
            if (opponentHairMaterialRef.color != LemmingCustomizer.Instance.player1HairColor) opponentHairMaterialRef.color = LemmingCustomizer.Instance.player1HairColor;
        }
    }
}

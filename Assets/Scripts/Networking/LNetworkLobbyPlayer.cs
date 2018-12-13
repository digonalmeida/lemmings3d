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

    public static LNetworkLobbyPlayer LocalInstance { get; private set; }
    public static LNetworkLobbyPlayer Player1Instance { get; private set; }
    public static LNetworkLobbyPlayer Player2Instance { get; private set; }

    //SyncVar Variables
    [SyncVar]
    public Player playerNum;
    [SyncVar]
    public string playerName;
    [SyncVar]
    public Color playerClothColor = Color.grey;
    [SyncVar]
    public Color playerHairColor = Color.grey;
    [SyncVar]
    public bool playerReady = false;

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

    //Start
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject); //If this line is removed all network ceases to work
        if (isServer)
        {
            if (isLocalPlayer)
            {
                LocalInstance = this;
                Player1Instance = this;
                this.name = name + "_Player1";
            }
            else
            {
                Player2Instance = this;
                this.name = name + "_Player2";
            }
        }
        else
        {
            if (isLocalPlayer)
            {
                LocalInstance = this;
                Player2Instance = this;
                this.name = name + "_Player2";
            }
            else
            {
                Player1Instance = this;
                this.name = name + "_Player1";
            }
        }
    }

    //Update Ready Checkbox on Start
    public override void OnStartClient()
    {
        base.OnStartClient();
        LobbyPanelManager.Instance.setPlayerReady(playerNum, playerReady);
    }

    //Get Player Num
    public Player getPlayerNum()
    {
        return playerNum;
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

    [ClientRpc]
    public void RpcFadeMusic()
    {
        if (MenuAudioManager.Instance != null) MenuAudioManager.Instance.stopMusic();
        else if (AudioManager.Instance != null) AudioManager.Instance.stopMusic();
    }

    //Update
    public void Update()
    {
        if(hasAuthority)
        {
            if (playerNum == Player.Player1)
            {
                if (Player1Instance != null && !playerName.Equals(LobbyPanelManager.Instance.player1Text.text)) LobbyPanelManager.Instance.player1Text.text = playerName;
                if (Player2Instance != null && !Player2Instance.playerName.Equals(LobbyPanelManager.Instance.player2Text.text)) LobbyPanelManager.Instance.player2Text.text = Player2Instance.playerName;
                if (playerClothMaterialRef.color != playerClothColor) playerClothMaterialRef.color = playerClothColor;
                if (playerHairMaterialRef.color != playerHairColor) playerHairMaterialRef.color = playerHairColor;
                if (Player2Instance != null && opponentClothMaterialRef.color != Player2Instance.playerClothColor) opponentClothMaterialRef.color = Player2Instance.playerClothColor;
                if (Player2Instance != null && opponentHairMaterialRef.color != Player2Instance.playerHairColor) opponentHairMaterialRef.color = Player2Instance.playerHairColor;
            }
            else if (playerNum == Player.Player2)
            {
                if (Player2Instance != null && !playerName.Equals(LobbyPanelManager.Instance.player2Text.text)) LobbyPanelManager.Instance.player2Text.text = playerName;
                if (Player1Instance != null && !Player1Instance.playerName.Equals(LobbyPanelManager.Instance.player1Text.text)) LobbyPanelManager.Instance.player1Text.text = Player1Instance.playerName;
                if (playerClothMaterialRef.color != playerClothColor) playerClothMaterialRef.color = playerClothColor;
                if (playerHairMaterialRef.color != playerHairColor) playerHairMaterialRef.color = playerHairColor;
                if (Player1Instance != null && opponentClothMaterialRef.color != Player1Instance.playerClothColor) opponentClothMaterialRef.color = Player1Instance.playerClothColor;
                if (Player1Instance != null && opponentHairMaterialRef.color != Player1Instance.playerHairColor) opponentHairMaterialRef.color = Player1Instance.playerHairColor;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyPanelManager : NetworkBehaviour
{
    //Network
    private LNetworkLobbyPlayer player;
    private LNetworkLobbyPlayer opponent;

    //References
    public GameObject player1Lemming;
    public GameObject player2Lemming;
    public Text player1Text;
    public Text player2Text;
    public GameObject previousColorPanelPlayer1;
    public GameObject previousColorPanelPlayer2;
    public GameObject nextColorPanelPlayer1;
    public GameObject nextColorPanelPlayer2;
    public Sprite readySprite;
    public Sprite unreadySprite;
    public Text readyButtonText;
    public Image readyPlayer1Checkbox;
    public Image readyPlayer2Checkbox;

    //Singleton
    private static LobbyPanelManager instance;
    public static LobbyPanelManager Instance
    {
        get
        {
            return instance;
        }
    }

    //On Object Awake
    private void Awake()
    {
        //Check Singleton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    //Set Player
    public void setColorChangeButtons(Player playerNum, bool activate)
    {
        if(playerNum == Player.Player1)
        {
            for (int i = 0; i < previousColorPanelPlayer1.transform.childCount; i++)
            {
                previousColorPanelPlayer1.transform.GetChild(i).gameObject.SetActive(activate);
            }
            for (int i = 0; i < nextColorPanelPlayer1.transform.childCount; i++)
            {
                nextColorPanelPlayer1.transform.GetChild(i).gameObject.SetActive(activate);
            }
        }
        else if (playerNum == Player.Player2)
        {
            for (int i = 0; i < previousColorPanelPlayer2.transform.childCount; i++)
            {
                previousColorPanelPlayer2.transform.GetChild(i).gameObject.SetActive(activate);
            }
            for (int i = 0; i < nextColorPanelPlayer2.transform.childCount; i++)
            {
                nextColorPanelPlayer2.transform.GetChild(i).gameObject.SetActive(activate);
            }
        }
    }

    //Request Previous Hair
    public void requestPreviousHair()
    {
        if (player == null) player = LNetworkLobbyPlayer.LocalInstance;
        player.CmdRequestPreviousHairColor();
    }

    //Request Next Hair
    public void requestNextHair()
    {
        if (player == null) player = LNetworkLobbyPlayer.LocalInstance;
        player.CmdRequestNextHairColor();
    }

    //Request Previous Cloth
    public void requestPreviousCloth()
    {
        if (player == null) player = LNetworkLobbyPlayer.LocalInstance;
        player.CmdRequestPreviousClothColor();
    }

    //Request Next Cloth
    public void requestNextCloth()
    {
        if (player == null) player = LNetworkLobbyPlayer.LocalInstance;
        player.CmdRequestNextClothColor();
    }

    //Request Set Ready of Local Player
    public void requestSetReady()
    {
        if (player == null) player = LNetworkLobbyPlayer.LocalInstance;
        if (player != null) player.CmdSetReadyOrUnready(!player.readyToBegin);
    }

    //Set Ready
    public void setPlayerReady(Player playerNum, bool ready)
    {
        //Update Everything & Inform the Server
        if (ready)
        {
            if(playerNum == Player.Player1) readyPlayer1Checkbox.sprite = readySprite;
            else if(playerNum == Player.Player2) readyPlayer2Checkbox.sprite = readySprite;

            if (player == null) player = LNetworkLobbyPlayer.LocalInstance;
            if (player != null && player.playerNum == playerNum)
            {
                readyButtonText.text = "Unready";
                player.playerReady = ready;
                player.SendReadyToBeginMessage();
                setColorChangeButtons(playerNum, false);
            }  
        }
        else
        {
            if (playerNum == Player.Player1) readyPlayer1Checkbox.sprite = unreadySprite;
            else if (playerNum == Player.Player2) readyPlayer2Checkbox.sprite = unreadySprite;

            if (player == null) player = LNetworkLobbyPlayer.LocalInstance;
            if (player != null && player.playerNum == playerNum)
            {
                readyButtonText.text = "Ready";
                player.playerReady = ready;
                player.SendNotReadyToBeginMessage();
                setColorChangeButtons(playerNum, true);
            } 
        }
    }

    //Network Integrity Functions
    public void resetPlayer2()
    {
        player2Text.text = "Waiting for Player";
        readyPlayer2Checkbox.sprite = unreadySprite;
        SkinnedMeshRenderer meshRenderer = player2Lemming.GetComponentInChildren<SkinnedMeshRenderer>();
        LemmingCustomizer.Instance.resetColorsAvailability(meshRenderer.materials[1].color, meshRenderer.materials[2].color);
        meshRenderer.materials[1].color = Color.gray;
        meshRenderer.materials[2].color = Color.gray;
    }
}

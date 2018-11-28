using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyPanelManager : NetworkBehaviour
{
    //Variables
    private bool readyToBegin = false;

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
    public Image readyButtonCheckbox;

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
    public void setPlayerButtons(Player playerNum)
    {
        if(playerNum == Player.Player1)
        {
            for (int i = 0; i < previousColorPanelPlayer2.transform.childCount; i++)
            {
                previousColorPanelPlayer2.transform.GetChild(i).gameObject.SetActive(false);
            }
            for (int i = 0; i < nextColorPanelPlayer2.transform.childCount; i++)
            {
                nextColorPanelPlayer2.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < previousColorPanelPlayer1.transform.childCount; i++)
            {
                previousColorPanelPlayer1.transform.GetChild(i).gameObject.SetActive(false);
            }
            for (int i = 0; i < nextColorPanelPlayer1.transform.childCount; i++)
            {
                nextColorPanelPlayer1.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    //Request Previous Hair
    public void requestPreviousHair()
    {
        if (player == null) player = LNetworkLobbyPlayer.getLocalLobbyPlayer();
        player.CmdRequestPreviousHairColor();
    }

    //Request Next Hair
    public void requestNextHair()
    {
        if (player == null) player = LNetworkLobbyPlayer.getLocalLobbyPlayer();
        player.CmdRequestNextHairColor();
    }

    //Request Previous Cloth
    public void requestPreviousCloth()
    {
        if (player == null) player = LNetworkLobbyPlayer.getLocalLobbyPlayer();
        player.CmdRequestPreviousClothColor();
    }

    //Request Next Cloth
    public void requestNextCloth()
    {
        if (player == null) player = LNetworkLobbyPlayer.getLocalLobbyPlayer();
        player.CmdRequestNextClothColor();
    }

    //Set Ready
    public void setReady()
    {
        //Update Ready Variable
        readyToBegin = !readyToBegin;

        if (player == null) player = LNetworkLobbyPlayer.getLocalLobbyPlayer();
        if (player != null)
        {
            //Update CheckBox Sprite
            if (readyToBegin) readyButtonCheckbox.sprite = readySprite;
            else readyButtonCheckbox.sprite = unreadySprite;

            //Update Lobby Player
            player.readyToBegin = readyToBegin;
        }
    }
}

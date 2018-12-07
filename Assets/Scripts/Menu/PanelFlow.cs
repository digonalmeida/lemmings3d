using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelFlow : MonoBehaviour
{
    //Panel References
    public GameObject loginPanel;
    public GameObject gameModePanel;
    public GameObject hostGamePanel;
    public GameObject lobbyPanel;
    public GameObject multiplayerPanel;

    //Elements References
    public InputField nameInputField;
    public Text changeNameButtonText;
    public InputField roomNameInputField;

    //Network
    LNetworkLobbyManager networkManager;
    LNetworkLobbyPlayer lobbyPlayer;
    
    //Start
    private void Start()
    {
        networkManager = LNetworkLobbyManager.singleton.GetComponent<LNetworkLobbyManager>();
    }

    //Check Name
    private bool checkName(string name)
    {
        return (!string.IsNullOrEmpty(name));
    }

    //Attempt to Register Name & Move to Main Menu
    public void registerName()
    {
        if (checkName(nameInputField.text))
        {
            UserData.name = nameInputField.text;
            changeNameButtonText.text = "Logged as: " + nameInputField.text;
            loginPanel.SetActive(false);
            gameModePanel.SetActive(true);
        }
        else nameInputField.placeholder.GetComponent<Text>().text = "Invalid Name";
    }

    //Create Match
    public void CreateMatch()
    {
        if (!string.IsNullOrEmpty(roomNameInputField.text))
        {
            networkManager.matchMaker.CreateMatch(roomNameInputField.text, 2, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
            loadLobby();
        }
        else roomNameInputField.placeholder.GetComponent<Text>().text = "Invalid Name";
    }

    //Load Lobby
    public void loadLobby()
    {
        multiplayerPanel.SetActive(false);
        hostGamePanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    //Quit Game
    public void quitGame()
    {
        Application.Quit();
    }
}

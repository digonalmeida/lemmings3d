using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LNetworkLobbyPlayer : NetworkLobbyPlayer
{
    //Variables
    private Color playerClothColor;
    private Color playerHairColor;

    //References
    private Text playerTextRef;
    private Material clothMaterialRef;
    private Material hairMaterialRef;

    // Use this for initialization
    void Start ()
    {
        //Set Initial References
		if(isServer)
        {
            playerTextRef = LobbyPanelManager.Instance.player1Text;
            playerTextRef.text = UserData.name;
            clothMaterialRef = LobbyPanelManager.Instance.player1Lemming.GetComponentInChildren<SkinnedMeshRenderer>().materials[1];
            hairMaterialRef = LobbyPanelManager.Instance.player1Lemming.GetComponentInChildren<SkinnedMeshRenderer>().materials[2];
            playerClothColor = Color.blue;
            playerHairColor = Color.green;
        }
        else
        {
            playerTextRef = LobbyPanelManager.Instance.player2Text;
            playerTextRef.text = UserData.name;
            clothMaterialRef = LobbyPanelManager.Instance.player2Lemming.GetComponentInChildren<SkinnedMeshRenderer>().materials[1];
            hairMaterialRef = LobbyPanelManager.Instance.player2Lemming.GetComponentInChildren<SkinnedMeshRenderer>().materials[2];
            playerClothColor = Color.red;
            playerHairColor = Color.black;
        }

        //Finally...
        changeColor(playerClothColor, playerHairColor);
    }
	
    //Change Color & Update Material
	public void changeColor(Color clothColor, Color hairColor)
    {
        playerTextRef.color = clothColor;
        clothMaterialRef.color = clothColor;
        hairMaterialRef.color = hairColor;
    }
}

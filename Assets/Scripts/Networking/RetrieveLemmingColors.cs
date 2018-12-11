using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrieveLemmingColors : MonoBehaviour
{
    //Variables
    public Player team = Player.None;
    private Material hairMaterialRef;
    private Material clothMaterialRef;

    // Use this for initialization
    void Start ()
    {
        //Get References
        SkinnedMeshRenderer RendererRef = this.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        hairMaterialRef = RendererRef.materials[2];
        clothMaterialRef = RendererRef.materials[1];

        //Set Colors
        if (team == Player.Player1)
        {
            hairMaterialRef.color = LNetworkLobbyPlayer.Player1Instance.playerHairColor;
            clothMaterialRef.color = LNetworkLobbyPlayer.Player1Instance.playerClothColor;
        }
        else if (team == Player.Player2)
        {
            hairMaterialRef.color = LNetworkLobbyPlayer.Player2Instance.playerHairColor;
            clothMaterialRef.color = LNetworkLobbyPlayer.Player2Instance.playerClothColor;
        }
    }
}

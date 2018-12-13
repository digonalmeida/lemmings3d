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

        var player = team == Player.Player1 ? LNetworkLobbyPlayer.Player1Instance : LNetworkLobbyPlayer.Player2Instance;

        if(player == null)
        {
            return;
        }

        hairMaterialRef.color = player.playerHairColor;
        clothMaterialRef.color = player.playerClothColor;
    }
}

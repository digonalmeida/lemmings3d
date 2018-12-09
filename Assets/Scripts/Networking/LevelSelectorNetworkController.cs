﻿using LevelMap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorNetworkController : MonoBehaviour
{
    //Variables
    private int selectedLevelPlayer1;
    private int selectedLevelPlayer2;

    //Singleton
    private static LevelSelectorNetworkController instance;
    public static LevelSelectorNetworkController Instance
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

    //Define & Return Level to Load
    public int getIndexMapAssetToLoad()
    {
        if (selectedLevelPlayer1 == selectedLevelPlayer2) return selectedLevelPlayer1;
        else
        {
            if(Random.value >= 0.5f) return selectedLevelPlayer1;
            else return selectedLevelPlayer2;
        }
    }

    //Select Level by Player
    public void selectLevel(int indexSelection, Player playerNum)
    {
        if(playerNum == Player.Player1) selectedLevelPlayer1 = indexSelection;
        else if (playerNum == Player.Player2) selectedLevelPlayer2 = indexSelection;
        LNetworkPlayer.getLocalPlayer().RpcSelectLevel(indexSelection, playerNum);
    }
}

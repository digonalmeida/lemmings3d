using LevelMap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorNetworkController : MonoBehaviour
{
    //Variables
    private int selectedLevelPlayer1;
    private int selectedLevelPlayer2;
    public GameObject levelSelectorContentPanel;

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
    public MapAsset getLevelToLoad()
    {
        if (selectedLevelPlayer1 == selectedLevelPlayer2) return levelSelectorContentPanel.transform.GetChild(selectedLevelPlayer1).GetComponent<LevelSelectButton>().getMapAsset();
        else
        {
            if(Random.value >= 0.5f) return levelSelectorContentPanel.transform.GetChild(selectedLevelPlayer1).GetComponent<LevelSelectButton>().getMapAsset();
            else return levelSelectorContentPanel.transform.GetChild(selectedLevelPlayer2).GetComponent<LevelSelectButton>().getMapAsset();
        }
    }

    //Select Level by Player
    public void selectLevel(int indexButton, Player playerNum)
    {
        if(playerNum == Player.Player1) selectedLevelPlayer1 = indexButton;
        else if (playerNum == Player.Player2) selectedLevelPlayer2 = indexButton;
        LNetworkPlayer.getLocalPlayer().RpcSelectLevel(selectedLevelPlayer1, selectedLevelPlayer2);
    }
}

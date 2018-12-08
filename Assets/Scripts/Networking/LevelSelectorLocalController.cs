using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorLocalController : MonoBehaviour
{
    //Variables
    public Sprite player1SelectSprite;
    public Sprite player2SelectSprite;
    public Sprite bothSelectSprite;
    public GameObject levelSelectorContentPanel;

    //Control Variables
    private GameObject selectedLevelPlayer1;
    private GameObject selectedLevelPlayer2;

    //Singleton
    private static LevelSelectorLocalController instance;
    public static LevelSelectorLocalController Instance
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

    //Update Toggle Button
    public void updateToggle(int indexPlayer1Selection, int indexPlayer2Selection, Player playerNum)
    {
        if(playerNum == Player.Player1)
        {

        }
        else if (playerNum == Player.Player2)
        {

        }
    }
}

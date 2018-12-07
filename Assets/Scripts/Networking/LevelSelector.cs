using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    //Variables
    private GameObject selectedLevelPlayer1;
    private GameObject selectedLevelPlayer2;
    public Sprite player1SelectSprite;
    public Sprite player2SelectSprite;
    public Sprite bothSelectSprite;

    //Singleton
    private static LevelSelector instance;
    public static LevelSelector Instance
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

    //Select Level
    public void selectLevel(GameObject level, Player playerNum)
    {
        if (playerNum == Player.Player1)
        {
            //Disable Previously Selected Level Selection
            if (selectedLevelPlayer1 != null)
            {
                selectedLevelPlayer1.GetComponent<Image>().enabled = false;
                selectedLevelPlayer1.transform.GetChild(1).GetComponent<Text>().enabled = false;
            }

            //Select New Level
            selectedLevelPlayer1 = level;

            //Update Selection Sprite
            if (selectedLevelPlayer1 == selectedLevelPlayer2) level.GetComponent<Image>().sprite = bothSelectSprite;
            else level.GetComponent<Image>().sprite = player1SelectSprite;

            //Enable Sprite & Text
            selectedLevelPlayer1.GetComponent<Image>().enabled = true;
            selectedLevelPlayer1.transform.GetChild(1).GetComponent<Text>().enabled = true;
        }
        else if (playerNum == Player.Player2)
        {
            //Disable Previously Selected Level Selection
            if (selectedLevelPlayer2 != null)
            {
                selectedLevelPlayer2.GetComponent<Image>().enabled = false;
                selectedLevelPlayer2.transform.GetChild(2).GetComponent<Text>().enabled = false;
            }

            //Select New Level
            selectedLevelPlayer2 = level;

            //Update Selection Sprite
            if (selectedLevelPlayer2 == selectedLevelPlayer1) level.GetComponent<Image>().sprite = bothSelectSprite;
            else level.GetComponent<Image>().sprite = player2SelectSprite;

            //Enable Sprite & Text
            selectedLevelPlayer2.GetComponent<Image>().enabled = true;
            selectedLevelPlayer2.transform.GetChild(2).GetComponent<Text>().enabled = true;
        }
    }
}

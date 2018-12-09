using LevelMap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorPanelController : MonoBehaviour
{
    //Control Variables
    private int selectedLevelPlayer1 = -1;
    private int selectedLevelPlayer2 = -1;
    private List<GameObject> levels;

    //References
    public GameObject levelSelectButtonPrefab;
    public Sprite player1SelectSprite;
    public Sprite player2SelectSprite;
    public Sprite bothSelectSprite;
    public GameObject readyButton;
    private LNetworkPlayer player;

    //Singleton
    private static LevelSelectorPanelController instance;
    public static LevelSelectorPanelController Instance
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

    // Use this for initialization
    void Start()
    {
        levels = new List<GameObject>();
        List<MapAsset> mapAssets = MapManager.Instance.MapAssets;
        for (int i = 0; i < mapAssets.Count; i++)
        {
            GameObject obj = Instantiate(levelSelectButtonPrefab, this.transform);
            obj.GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
            LevelSelectButton button = obj.GetComponent<LevelSelectButton>();
            button.setMapAsset(mapAssets[i].mapScrenshot, i);
            levels.Add(obj);
        }

        //Update Size
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 170 * Mathf.CeilToInt(mapAssets.Count / 4f));
    }

    //Set Player Ready
    public void setPlayerReadyOrUnready()
    {
        if (player == null) player = LNetworkPlayer.getLocalPlayer();
        if (player != null)
        {
            if (player.levelSelectReady)
            {
                player.CmdInformReadyStatus(false);
                readyButton.GetComponent<Image>().color = Color.green;
                readyButton.transform.GetChild(0).GetComponent<Text>().text = "Ready";
            }
            else
            {
                player.CmdInformReadyStatus(true);
                readyButton.GetComponent<Image>().color = Color.yellow;
                readyButton.transform.GetChild(0).GetComponent<Text>().text = "Unready";
            }
        }
    }

    //Update Toggle Button
    public void selectLevel(int indexSelection, Player playerNum)
    {
        if(playerNum == Player.Player1)
        {
            //Disable Previously Selected Level Selection
            if (selectedLevelPlayer1 != -1)
            {
                if (selectedLevelPlayer2 == selectedLevelPlayer1) levels[selectedLevelPlayer1].GetComponent<Image>().sprite = player2SelectSprite;
                else levels[selectedLevelPlayer1].GetComponent<Image>().enabled = false;

                levels[selectedLevelPlayer1].transform.GetChild(1).gameObject.SetActive(false);
            }

            //Select New Level
            selectedLevelPlayer1 = indexSelection;

            //Update Selection Sprite
            if (selectedLevelPlayer1 == selectedLevelPlayer2) levels[indexSelection].GetComponent<Image>().sprite = bothSelectSprite;
            else levels[indexSelection].GetComponent<Image>().sprite = player1SelectSprite;

            //Enable Sprite & Text
            levels[indexSelection].GetComponent<Image>().enabled = true;
            levels[indexSelection].transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (playerNum == Player.Player2)
        {
            //Disable Previously Selected Level Selection
            if (selectedLevelPlayer2 != -1)
            {
                if(selectedLevelPlayer2 == selectedLevelPlayer1) levels[selectedLevelPlayer1].GetComponent<Image>().sprite = player1SelectSprite;
                else levels[selectedLevelPlayer2].GetComponent<Image>().enabled = false;

                levels[selectedLevelPlayer2].transform.GetChild(2).gameObject.SetActive(false);
            }

            //Select New Level
            selectedLevelPlayer2 = indexSelection;

            //Update Selection Sprite
            if (selectedLevelPlayer1 == selectedLevelPlayer2) levels[indexSelection].GetComponent<Image>().sprite = bothSelectSprite;
            else levels[indexSelection].GetComponent<Image>().sprite = player2SelectSprite;

            //Enable Sprite & Text
            levels[indexSelection].GetComponent<Image>().enabled = true;
            levels[indexSelection].transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}

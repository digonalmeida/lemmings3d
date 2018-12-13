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
        if (LNetworkPlayer.LocalInstance != null)
        {
            if (LNetworkPlayer.LocalInstance.levelSelectReady)
            {
                LNetworkPlayer.LocalInstance.CmdInformReadyStatus(false);
                readyButton.GetComponent<Image>().color = Color.green;
                readyButton.transform.GetChild(0).GetComponent<Text>().text = "Ready";
            }
            else
            {
                LNetworkPlayer.LocalInstance.CmdInformReadyStatus(true);
                readyButton.GetComponent<Image>().color = Color.yellow;
                readyButton.transform.GetChild(0).GetComponent<Text>().text = "Unready";
            }
        }
    }

    //Update
    private void Update()
    {
        if(LNetworkPlayer.Player1Instance != null && LNetworkPlayer.Player2Instance != null)
        {
            //Check for Changes
            if (LNetworkPlayer.Player1Instance.selectedLevel != selectedLevelPlayer1 && LNetworkPlayer.Player2Instance.selectedLevel != selectedLevelPlayer2)
            {
                //Reset Last Selection
                if (selectedLevelPlayer1 != -1)
                {
                    levels[selectedLevelPlayer1].GetComponent<Image>().enabled = false;
                    levels[selectedLevelPlayer1].transform.GetChild(1).gameObject.SetActive(false);
                }
                if (selectedLevelPlayer2 != -1)
                {
                    levels[selectedLevelPlayer1].GetComponent<Image>().enabled = false;
                    levels[selectedLevelPlayer1].transform.GetChild(2).gameObject.SetActive(false);
                }

                //Update Control Variables
                selectedLevelPlayer1 = LNetworkPlayer.Player1Instance.selectedLevel;
                selectedLevelPlayer2 = LNetworkPlayer.Player2Instance.selectedLevel;

                //Update Sprite & Text
                levels[LNetworkPlayer.Player1Instance.selectedLevel].GetComponent<Image>().sprite = bothSelectSprite;
                levels[LNetworkPlayer.Player1Instance.selectedLevel].GetComponent<Image>().enabled = true;
                levels[LNetworkPlayer.Player1Instance.selectedLevel].transform.GetChild(1).gameObject.SetActive(true);
                levels[LNetworkPlayer.Player1Instance.selectedLevel].transform.GetChild(2).gameObject.SetActive(true);
            }
            else if (LNetworkPlayer.Player1Instance.selectedLevel != selectedLevelPlayer1)
            {
                //Reset Last Selection
                if (selectedLevelPlayer1 != -1)
                {
                    if (selectedLevelPlayer2 == selectedLevelPlayer1) levels[selectedLevelPlayer1].GetComponent<Image>().sprite = player2SelectSprite;
                    else levels[selectedLevelPlayer1].GetComponent<Image>().enabled = false;
                    levels[selectedLevelPlayer1].transform.GetChild(1).gameObject.SetActive(false);
                }

                //Update Control Variables
                selectedLevelPlayer1 = LNetworkPlayer.Player1Instance.selectedLevel;

                //Update Sprite & Text
                if(LNetworkPlayer.Player1Instance.selectedLevel == LNetworkPlayer.Player2Instance.selectedLevel) levels[LNetworkPlayer.Player1Instance.selectedLevel].GetComponent<Image>().sprite = bothSelectSprite;
                else levels[LNetworkPlayer.Player1Instance.selectedLevel].GetComponent<Image>().sprite = player1SelectSprite;
                levels[LNetworkPlayer.Player1Instance.selectedLevel].GetComponent<Image>().enabled = true;
                levels[LNetworkPlayer.Player1Instance.selectedLevel].transform.GetChild(1).gameObject.SetActive(true);
            }
            else if (LNetworkPlayer.Player2Instance.selectedLevel != selectedLevelPlayer2)
            {
                //Reset Last Selection
                if (selectedLevelPlayer2 != -1)
                {
                    if (selectedLevelPlayer2 == selectedLevelPlayer1) levels[selectedLevelPlayer2].GetComponent<Image>().sprite = player1SelectSprite;
                    else levels[selectedLevelPlayer2].GetComponent<Image>().enabled = false;
                    levels[selectedLevelPlayer2].transform.GetChild(2).gameObject.SetActive(false);
                }

                //Update Control Variables
                selectedLevelPlayer2 = LNetworkPlayer.Player2Instance.selectedLevel;

                //Update Sprite & Text
                if (LNetworkPlayer.Player1Instance.selectedLevel == LNetworkPlayer.Player2Instance.selectedLevel) levels[LNetworkPlayer.Player2Instance.selectedLevel].GetComponent<Image>().sprite = bothSelectSprite;
                else levels[LNetworkPlayer.Player2Instance.selectedLevel].GetComponent<Image>().sprite = player2SelectSprite;
                levels[LNetworkPlayer.Player2Instance.selectedLevel].GetComponent<Image>().enabled = true;
                levels[LNetworkPlayer.Player2Instance.selectedLevel].transform.GetChild(2).gameObject.SetActive(true);
            }
        }
    }
}

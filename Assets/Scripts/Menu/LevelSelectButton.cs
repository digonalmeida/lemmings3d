using LevelMap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    //Variables
    private Image imageReference;
    private MapAsset levelAsset;
    private int indexButton;

    //Start
    private void Start()
    {
        imageReference = GetComponentInChildren<Image>();
    }

    //Update Image with level screenshot
    public void setMapAsset(MapAsset level, int index)
    {
        if (level != null)
        {
            levelAsset = level;
            if(level.mapScrenshot != null) imageReference.sprite = level.mapScrenshot;
            indexButton = index;
        }
    }

    //Get Map Asset
    public MapAsset getMapAsset()
    {
        return levelAsset;
    }

    //Select Level
    public void selectLevel()
    {
        if (GetComponent<Toggle>().isOn)
        {
            LNetworkPlayer player = LNetworkPlayer.getLocalPlayer();
            player.CmdSelectLevel(indexButton);
        }
    }

}

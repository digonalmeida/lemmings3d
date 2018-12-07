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

    //Start
    private void Start()
    {
        imageReference = GetComponentInChildren<Image>();
    }

    //Update Image with level screenshot
    public void setMapAsset(MapAsset level)
    {
        if (level != null)
        {
            levelAsset = level;
            if(level.mapScrenshot != null) imageReference.sprite = level.mapScrenshot;
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
        if(GetComponent<Toggle>().isOn) LNetworkPlayer.getLocalPlayer().CmdSelectLevel(this.gameObject);
    }

}

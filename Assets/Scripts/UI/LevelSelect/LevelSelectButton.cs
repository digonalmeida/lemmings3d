using LevelMap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    //Variables
    private Image imageReference;
    private int indexMapAsset;

    //Start
    private void Start()
    {
        imageReference = GetComponentInChildren<Image>();
    }

    //Update Image with level screenshot
    public void setMapAsset(Sprite levelScreenshot, int index)
    {
        if(levelScreenshot != null) imageReference.sprite = levelScreenshot;
        indexMapAsset = index;
    }

    //Get Map Asset
    public int getIndexMapAsset()
    {
        return indexMapAsset;
    }

    //Select Level
    public void selectLevel()
    {
        if (GetComponent<Toggle>().isOn)
        {
            if(LNetworkPlayer.LocalInstance != null && !LNetworkPlayer.LocalInstance.levelSelectReady) LNetworkPlayer.LocalInstance.CmdSelectLevel(indexMapAsset);
        }
    }

}

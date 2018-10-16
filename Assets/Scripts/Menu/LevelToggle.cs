using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelToggle : MonoBehaviour
{
    //Variables
    public string levelName;
    public LevelSelector levelSelectorRef;
    public int levelId;

    //Change Selected Level
    public void changeSelectedLevel(bool active)
    {
        if(active)
        {
            this.GetComponent<Image>().color = Color.red;
            levelSelectorRef.changeSelectedLevel(levelId);
        }
        else
        {
            this.GetComponent<Image>().color = Color.white;
        }
    }
}

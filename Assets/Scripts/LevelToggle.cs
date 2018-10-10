using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelToggle : MonoBehaviour
{
    //Variables
    public string levelName;
    public LevelSelector levelSelectorRef;

    //Change Selected Level
    public void changeSelectedLevel(bool active)
    {
        if(active)
        {
            this.GetComponent<Image>().color = Color.red;
            levelSelectorRef.changeSelectedLevel(levelName);
        }
        else
        {
            this.GetComponent<Image>().color = Color.white;
        }
    }
}

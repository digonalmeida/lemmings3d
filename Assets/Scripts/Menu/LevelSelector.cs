using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    //Variables
    private int selectedLevel;

    //Start
    private void Start()
    {
        selectedLevel = 0;
    }

    //Change Selected Level
    public void changeSelectedLevel(int id)
    {
        selectedLevel = id;
    }

    //Load Level
    public void loadLevel()
    {
        GameManager.Instance.LoadLevelById(selectedLevel);
    }
}

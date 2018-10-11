using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    //Variables
    private string selectedLevel;

    //Start
    private void Start()
    {
        selectedLevel = "Level1";
    }

    //Change Selected Level
    public void changeSelectedLevel(string newLevel)
    {
        selectedLevel = newLevel;
    }

    //Load Level
    public void loadLevel()
    {
        Debug.Log("We need more puzzles!");
        //SceneManager.LoadScene(selectedLevel, LoadSceneMode.Single);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelFlow : MonoBehaviour
{
    //Variables
    public GameObject[] panels;
    private int activePanel;

    //Start
    private void Start()
    {
        activePanel = 0;
    }

    //Change Panel Method
    public void changePanel(int nextPanel)
    {
        panels[activePanel].SetActive(false);
        panels[nextPanel].SetActive(true);
        activePanel = nextPanel;
    }

    //Quit Game
    public void quitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelFlow : MonoBehaviour
{
    //Variables
    public GameObject[] panels;
    private int activePanel;

    //References
    public InputField nameInputField;
    public Text changeNameButtonText;

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

    //Check Name
    private bool checkName(string name)
    {
        return (!string.IsNullOrEmpty(name));
    }

    //Attempt to Register Name & Move to Main Menu
    public void registerName()
    {
        if(checkName(nameInputField.text))
        {
            UserData.name = nameInputField.text;
            changeNameButtonText.text = "Logged as: " + nameInputField.text;
            changePanel(1);
        }
    }

    //Quit Game
    public void quitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelFlow : MonoBehaviour
{
    //Variables
    public GameObject[] panels;

    //Change Panel Method
    public void changePanel(int currentPanel, int nextPanel)
    {
        panels[currentPanel].SetActive(false);
        panels[nextPanel].SetActive(true);
    }
}

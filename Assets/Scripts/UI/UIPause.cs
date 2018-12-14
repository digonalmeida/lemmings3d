using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPause : MonoBehaviour {

    public Canvas pauseCanvas;
    public Image backgroundFaderImage;
    public GameObject pausePanel;
    public Button firstSelected;


    public void Pause()
    {
        pauseCanvas.enabled = true;
        backgroundFaderImage.enabled = true;
        pausePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 0f;
    }

    public void UnPause()
    {
        pauseCanvas.enabled = false;
        backgroundFaderImage.enabled = false;
        pausePanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        pauseCanvas.enabled = false;
        backgroundFaderImage.enabled = false;
        pausePanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}

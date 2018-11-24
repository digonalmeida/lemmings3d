using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName
{
    Menu,
    Lobby,
    Level
}

public class SceneLoader : MonoBehaviour 
{
	//Reference
	public SceneName sceneToLoad;

	// Use this for initialization
	void Start ()
	{
        if (sceneToLoad == SceneName.Menu)
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
        else if (sceneToLoad == SceneName.Level)
        {
            SceneManager.LoadScene("DefaultLevel", LoadSceneMode.Single);
        }
    }
}

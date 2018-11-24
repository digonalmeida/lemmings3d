using LevelMap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private static string resourceName = "GameManager";
    private static GameManager instance = null;

    [SerializeField]
    public string gameplaySceneName;

    [SerializeField]
    private bool loadAssetsOnLoad = false;
    
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = CreateInstance();
            }

            return instance;
        }
    }

    public bool LoadAssetsOnLoad
    {
        get
        {
            return loadAssetsOnLoad;
        }
        private set
        {
            loadAssetsOnLoad = value;
        }
    }

    public static GameManager CreateInstance()
    {
        var resource = Resources.Load<GameObject>(resourceName);
        if (resource == null)
        {
            Debug.LogError("No GameManager found in resources!");
            return null;
        }

        var gameObjectInstance = Instantiate(resource);
        var gameManagerInstance = gameObjectInstance.GetComponent<GameManager>();

        if (gameManagerInstance == null)
        {
            Debug.LogError("Game Manager resource has no game manager component");
            Destroy(gameObjectInstance);
            return null;
        }

        gameObjectInstance.name = "_" + resourceName;
        gameObjectInstance.transform.SetAsFirstSibling();

        return gameManagerInstance;
    }

    public void Awake()
    {
        var managers = FindObjectsOfType<GameManager>();
        for (int i = 0; i < managers.Length; i++)
        {
            if(managers[i] != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool LoadLevelById(int id)
    {
        if (!MapManager.Instance.TrySelectMapById(id))
        {
            return false;
        }

        LoadAssetsOnLoad = true;

        SceneManager.LoadScene(gameplaySceneName);
        return false;
    }
}

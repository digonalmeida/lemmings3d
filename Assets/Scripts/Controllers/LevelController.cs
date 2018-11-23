using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelMap;
using UnityEngine.SceneManagement;

public class LevelController : Singleton<LevelController>
{
    public int lemmingsSpawned { get; private set; }
    public int lemmingsEnteredExit { get; private set; }
    public List<LemmingAI> lemmingsOnScene = new List<LemmingAI>();

    // settings
    private int currentRate = 50;
    public int CurrentRate { get { return currentRate; } }
    private MapSettings currentMapSettings;
    public MapSettings CurrentMapSettings { get { return currentMapSettings; } }

    private void OnEnable()
    {
        GameEvents.Lemmings.LemmingReachedExit += LemmingExit;
        GameEvents.Lemmings.LemmingSpawned += LemmingEnter;
        GameEvents.GameState.OnLoadGame += ResetVariables;
    }

    private void OnDisable()
    {
        GameEvents.Lemmings.LemmingReachedExit -= LemmingExit;
        GameEvents.Lemmings.LemmingSpawned -= LemmingEnter;
        GameEvents.GameState.OnLoadGame -= ResetVariables;
    }

    protected override void Awake()
    {
        base.Awake();
        ResetVariables();
    }

    private void Start()
    {
        if (GameManager.Instance.LoadAssetsOnLoad)
        {
            LoadGame();
        }
    }

    public void LemmingExit(LemmingAI lemming)
    {
        lemmingsOnScene.Remove(lemming);
        lemmingsEnteredExit++;
    }

    public void LemmingEnter(LemmingAI lemming)
    {
        lemmingsOnScene.Add(lemming);
        lemmingsSpawned++;
    }

    public void SetLevelConditions()
    {
        var settings = MapManager.Instance.SelectedMapAsset.Settings;
        if(settings == null)
        {
            return;
        }

        currentMapSettings = new MapSettings(settings);
    }


    public void ChangeSpawnRate(int increment)
    {
        if ((currentRate + increment) >= currentMapSettings.MinimumSpawnRate && (currentRate + increment) <= currentMapSettings.MaximumSpawnRate)
        {
            currentRate += increment;
            GameEvents.Lemmings.ChangedSpawnRate.SafeInvoke(currentRate);
        }
    }

    public void ResetVariables()
    {
        lemmingsSpawned = 0;
        lemmingsEnteredExit = 0;
        lemmingsOnScene.Clear();

        SetLevelConditions();
    }

    public void LoadGame()
    {
        Debug.Log("load game");
        GameEvents.GameState.OnLoadGame.SafeInvoke();
    }

    public void StartGame()
    {
        Debug.Log("Start Game");
        GameEvents.GameState.OnStartGame.SafeInvoke();
    }

    public void EndGame()
    {
        Debug.Log("End Game");
        GameEvents.GameState.OnEndGame.SafeInvoke();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

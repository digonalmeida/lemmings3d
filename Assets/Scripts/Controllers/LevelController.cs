using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelMap;
using UnityEngine.SceneManagement;

public class LevelController : Singleton<LevelController>
{
    // lemmings control
    public int lemmingsSpawned { get; private set; }
    public int lemmingsEnteredExit { get; private set; }
    public List<LemmingAI> lemmingsOnScene = new List<LemmingAI>();
    public int currentSpawnRate { get; private set; }

    // timer
    public float remainingTime { get; private set; }
    public float elapsedTime { get { return currentMapSettings.LevelTime - remainingTime; } }
    private bool levelStarted = false;
    private bool levelFinished = false;
    public bool inGame { get { return levelStarted && !levelFinished; } }

    // settings

    private MapSettings currentMapSettings = new MapSettings();
    public MapSettings CurrentMapSettings { get { return currentMapSettings; } }

    private void OnEnable()
    {
        GameEvents.Lemmings.LemmingReachedExit += LemmingExit;
        GameEvents.Lemmings.LemmingSpawned += LemmingEnter;
        GameEvents.GameState.OnLoadGame += LoadLevelSettings;
        GameEvents.GameState.OnStartGame += OnStartGame;
        GameEvents.GameState.OnEndGame += OnEndGame;

    }

    private void OnDisable()
    {
        GameEvents.Lemmings.LemmingReachedExit -= LemmingExit;
        GameEvents.Lemmings.LemmingSpawned -= LemmingEnter;
        GameEvents.GameState.OnLoadGame -= LoadLevelSettings;
        GameEvents.GameState.OnStartGame -= OnStartGame;
        GameEvents.GameState.OnEndGame -= OnEndGame;
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

    private void Update()
    {
        if (inGame)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime < 0)
            {
                Debug.Log("invoked game end");
                GameEvents.GameState.OnEndGame.SafeInvoke();
            }
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

    public void LoadLevelSettings()
    {
        var settings = MapManager.Instance.SelectedMapAsset.Settings;
        if (settings == null)
        {
            return;
        }

        ResetVariables();
        currentMapSettings = new MapSettings(settings);
        remainingTime = currentMapSettings.LevelTime;
        currentSpawnRate = currentMapSettings.StartSpawnRate;

        Debug.Log("Level settings loaded");
    }


    public void ChangeSpawnRate(int increment)
    {
        if ((currentSpawnRate + increment) >= currentMapSettings.MinimumSpawnRate && (currentSpawnRate + increment) <= currentMapSettings.MaximumSpawnRate)
        {
            currentSpawnRate += increment;
            GameEvents.Lemmings.ChangedSpawnRate.SafeInvoke();
        }
    }

    public void ResetVariables()
    {
        lemmingsSpawned = 0;
        lemmingsEnteredExit = 0;
        lemmingsOnScene.Clear();
        remainingTime = 0;
        levelStarted = false;
        levelFinished = false;
    }

    private void OnStartGame()
    {
        Debug.Log("Start game");
        levelStarted = true;
    }

    private void OnEndGame()
    {
        Debug.Log("End Game");
        levelFinished = true;
    }



    public void LoadGame()
    {
        GameEvents.GameState.OnLoadGame.SafeInvoke();
    }

    public void StartGame()
    {
        GameEvents.GameState.OnStartGame.SafeInvoke();
    }

    public void EndGame()
    {
        GameEvents.GameState.OnEndGame.SafeInvoke();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

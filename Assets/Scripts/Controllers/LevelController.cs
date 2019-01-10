using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelMap;
using UnityEngine.SceneManagement;

public class LevelController : Singleton<LevelController>
{
    // lemmings control

    public int currentSpawnRateIndex { get; private set; }
    public Player team = Player.Player1;
    public List<float> spawnLemmingsPerSecondRates = new List<float>() { 1f, 1f, 1f }; //Change on Level Manager Prefab

    // timer
    public float remainingTime { get { return gameStateManager.RemainingTime; } }
    public float ElapsedTime { get { return currentMapSettings.LevelTime - remainingTime; } }
    private bool levelStarted { get { return gameStateManager.LevelStarted; } }
    private bool levelFinished { get { return gameStateManager.LevelFinished; } }
    public bool inGame { get { return levelStarted && !levelFinished; } }

    // settings
    private MapSettings currentMapSettings = new MapSettings();
    public MapSettings CurrentMapSettings { get { return currentMapSettings; } }

    public NetworkGameStateManager gameStateManager { get; private set; }


    private void OnEnable()
    {
        GameEvents.GameState.OnLoadGame += LoadLevelSettings;
    }

    private void OnDisable()
    {
        GameEvents.GameState.OnLoadGame -= LoadLevelSettings;
    }

    protected override void Awake()
    {
        base.Awake();

        // load player team
        LNetworkLobbyPlayer netPlayer = LNetworkLobbyPlayer.LocalInstance;
        if (netPlayer != null)
        {
            team = netPlayer.playerNum;
            gameStateManager = netPlayer.gameObject.GetComponent<NetworkGameStateManager>();
        }
        else
        {
            team = Player.Player1;
        }
    }

    public void LoadLevelSettings()
    {
        var settings = MapManager.Instance.SelectedMapAsset.Settings;
        if (settings == null)
        {
            return;
        }

        currentMapSettings = new MapSettings(settings);
        currentSpawnRateIndex = 1;

        Debug.Log("Level settings loaded");
    }


    public void ChangeSpawnRate(int increment)
    {
        if ((currentSpawnRateIndex + increment) >= 0 && (currentSpawnRateIndex + increment) <= spawnLemmingsPerSecondRates.Count - 1)
        {
            currentSpawnRateIndex += increment;
            LNetworkPlayer.LocalInstance.SetSpawnRate(currentSpawnRateIndex);
        }
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

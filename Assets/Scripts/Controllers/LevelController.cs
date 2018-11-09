using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelMap;
using UnityEngine.SceneManagement;

public class LevelController : Singleton<LevelController>
{


    public static int lemmingsSpawned { get; private set; }
    public static int lemmingsEnteredExit { get; private set; }

    public static int minimumSpawnRate = 30;
    public static int maximumSpawnRate = 70;
    public static int currentRate = 50;

    private void OnEnable()
    {
        GameEvents.Lemmings.LemmingReachedExit += LemmingExit;
        GameEvents.Lemmings.LemmingSpawned += LemmingEnter;
    }

    private void OnDisable()
    {
        GameEvents.Lemmings.LemmingReachedExit -= LemmingExit;
        GameEvents.Lemmings.LemmingSpawned -= LemmingEnter;
    }

    private void Awake()
    {
        lemmingsSpawned = 0;
        lemmingsEnteredExit = 0;
    }

    private void Start()
    {
        if (GameManager.Instance.LoadAssetsOnLoad)
        {
            LoadGame();
        }
    }

    public void LemmingExit()
    {
        lemmingsEnteredExit++;
    }

    public void LemmingEnter()
    {
        lemmingsSpawned++;
    }



    public static void ChangeSpawnRate(int increment)
    {
        if ((currentRate + increment) >= minimumSpawnRate && (currentRate + increment) <= maximumSpawnRate)
        {
            currentRate += increment;
            GameEvents.Lemmings.TriggerSpawnRateChange(currentRate);
        }
    }


    public void LoadGame()
    {
        Debug.Log("load game");
        GameEvents.GameState.TriggerOnLoadGame();
    }

    public void StartGame()
    {
        Debug.Log("Start Game");
        GameEvents.GameState.TriggerOnStartGame();
    }

    public void EndGame()
    {
        Debug.Log("End Game");
        GameEvents.GameState.TriggerOnEndGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelMap;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public delegate void LemmingDidAction();
    public delegate void SpawnRateAction(int newRate);
    public delegate void GameStateAction();
    public static event LemmingDidAction LemmingReachedExit;
    public static event LemmingDidAction LemmingSpawned;
    public static event LemmingDidAction LemmingUsedSkill;
    public static event SpawnRateAction ChangedSpawnRate;

    public static event GameStateAction OnStartGame;
    public static event GameStateAction OnEndGame;
    public static event GameStateAction OnLoadGame;

    public static int lemmingsSpawned { get; private set; }
    public static int lemmingsEnteredExit { get; private set; }

    public static int minimumSpawnRate = 30;
    public static int maximumSpawnRate = 70;
    public static int currentRate = 50;

    private void OnEnable()
    {
        LemmingReachedExit += LemmingExit;
        LemmingSpawned += LemmingEnter;
    }

    private void OnDisable()
    {
        LemmingReachedExit -= LemmingExit;
        LemmingSpawned -= LemmingEnter;
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

    public static void TriggerLemmingReachedExit()
    {
        if(LemmingReachedExit!=null)
            LemmingReachedExit.Invoke();
    }

    public static void TriggerLemmingSpawned()
    {
        if (LemmingSpawned != null)
            LemmingSpawned.Invoke();
    }

    public static void TriggerLemmingUsedSkill()
    {
        if (LemmingUsedSkill != null)
            LemmingUsedSkill.Invoke();
    }

    public static void ChangeSpawnRate(int increment)
    {
        if ((currentRate + increment) >= minimumSpawnRate && (currentRate + increment) <= maximumSpawnRate)
        {
            currentRate += increment;
            TriggerSpawnRateChange(currentRate);
        }
    }

    public static void TriggerSpawnRateChange(int newRate)
    {
        if (ChangedSpawnRate != null)
            ChangedSpawnRate.Invoke(newRate);
    }

    public void LoadGame()
    {
        Debug.Log("load game");
        if (OnLoadGame != null)
        {
            OnLoadGame.Invoke();
        }
    }

    public void StartGame()
    {
        Debug.Log("Start Game");
        if (OnStartGame != null)
        {
            OnStartGame.Invoke();
        }
    }

    public void EndGame()
    {
        Debug.Log("End Game");
        if (OnEndGame !=  null)
        {
            OnEndGame.Invoke();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

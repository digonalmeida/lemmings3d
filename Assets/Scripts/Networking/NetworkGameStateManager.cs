using System;
using System.Collections;
using System.Collections.Generic;
using LevelMap;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkGameStateManager : NetworkBehaviour
{

    // time
    [SyncVar] private float remainingTime;

    // level state
    [SyncVar] private bool levelStarted = false;
    [SyncVar] private bool levelFinished = false;
    private bool inGame { get { return levelStarted && !levelFinished; } }

    // lemmings
    public Dictionary<Player, int> lemmingsSpawned { get; private set; }
    public Dictionary<Player, int> lemmingsDied { get; private set; }
    public Dictionary<Player, int> lemmingsEnteredExit { get; private set; }
    public Dictionary<Player, List<LemmingStateController>> lemmingsOnScene;



    private void OnEnable()
    {
        GameEvents.Lemmings.LemmingReachedExit += LemmingExit;
        GameEvents.Lemmings.LemmingSpawned += LemmingEnter;
        GameEvents.GameState.OnStartGame += OnStartGame;
        GameEvents.GameState.OnEndGame += OnEndGame;
        GameEvents.GameState.OnLoadGame += OnLoadGame;
    }

    private void OnDisable()
    {
        GameEvents.Lemmings.LemmingReachedExit -= LemmingExit;
        GameEvents.Lemmings.LemmingSpawned -= LemmingEnter;
        GameEvents.GameState.OnStartGame -= OnStartGame;
        GameEvents.GameState.OnEndGame -= OnEndGame;
        GameEvents.GameState.OnLoadGame -= OnLoadGame;
    }


    // getters
    public float RemainingTime
    {
        get
        {
            return remainingTime;
        }
    }

    public bool LevelStarted
    {
        get
        {
            return levelStarted;
        }
    }

    public bool LevelFinished
    {
        get
        {
            return levelFinished;
        }
    }

    private void Awake()
    {
        lemmingsSpawned = new Dictionary<Player, int>();
        lemmingsDied = new Dictionary<Player, int>();
        lemmingsEnteredExit = new Dictionary<Player, int>();
        lemmingsOnScene = new Dictionary<Player, List<LemmingStateController>>();
    }

    private void Update()
    {
        // server check winning and lose conditions
        if (isServer)
        {

            if (inGame)
            {
                // check timer
                remainingTime -= Time.deltaTime;
                if (remainingTime < 0)
                {
                    Debug.Log("Call Rpc Time Ended");
                    RpcTimeEnded();
                }

                // check lemings on final
                foreach (var item in lemmingsEnteredExit)
                {
                    if (item.Value >= LevelController.Instance.CurrentMapSettings.MinimumVictoryCount)
                    {
                        Debug.Log("Call Rpc Player Win");
                        RpcPlayerWin(item.Key);
                        break;
                    }
                }

                // check lemmings dead
                bool lemmingAlive = false;
                foreach (var item in lemmingsDied)
                {
                    if (item.Value < LevelController.Instance.CurrentMapSettings.LemmingsCount)
                    {
                        lemmingAlive = true;
                        break;
                    }
                }
                if (!lemmingAlive)
                {
                    Debug.Log("Call Rpc both players lose");
                    RpcBothPlayersLose();
                }
            }
        }
    }

    public void ResetVariables()
    {
        // clear
        lemmingsSpawned.Clear();
        lemmingsEnteredExit.Clear();
        lemmingsOnScene.Clear();

        // initialize dictionaries
        lemmingsSpawned = new Dictionary<Player, int>() { { Player.Player1, 0 }, { Player.Player2, 0 } };
        lemmingsDied = new Dictionary<Player, int>() { { Player.Player1, 0 }, { Player.Player2, 0 } };
        lemmingsEnteredExit = new Dictionary<Player, int>() { { Player.Player1, 0 }, { Player.Player2, 0 } };
        lemmingsOnScene = new Dictionary<Player, List<LemmingStateController>> { { Player.Player1, new List<LemmingStateController>() }, { Player.Player2, new List<LemmingStateController>() } };
    }


    private void OnLoadGame()
    {
        ResetVariables();
    }

    private void OnStartGame()
    {
        if (isServer)
        {
            SetupGame();
            levelStarted = true;
        }
    }

    public void SetupGame()
    {
        remainingTime = LevelController.Instance.CurrentMapSettings.LevelTime;
    }

    private void OnEndGame()
    {
        if (isServer)
        {
            levelFinished = true;
        }
    }

    private void LemmingExit(LemmingStateController lemming)
    {
        if (!lemmingsOnScene.ContainsKey(lemming.Team)) lemmingsOnScene.Add(lemming.Team, new List<LemmingStateController>());

        lemmingsOnScene[lemming.Team].Remove(lemming);

        lemmingsEnteredExit[lemming.Team]++;

    }

    private void LemmingEnter(LemmingStateController lemming)
    {
        lemmingsOnScene[lemming.Team].Add(lemming);

        lemmingsSpawned[lemming.Team]++;
    }



    [ClientRpc]
    private void RpcPlayerWin(Player player)
    {
        GameEvents.GameState.OnEndGame.SafeInvoke();
        GameEvents.GameState.OnPlayerWin.SafeInvoke(player);
    }

    [ClientRpc]
    private void RpcBothPlayersLose()
    {
        GameEvents.GameState.OnEndGame.SafeInvoke();
        GameEvents.GameState.OnBothPlayersLose.SafeInvoke();
    }

    [ClientRpc]
    private void RpcTimeEnded()
    {
        GameEvents.GameState.OnEndGame.SafeInvoke();
        GameEvents.GameState.OnBothPlayersLose.SafeInvoke();
    }

}

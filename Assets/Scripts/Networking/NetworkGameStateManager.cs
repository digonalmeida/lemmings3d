﻿using System;
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
    private LNetworkLobbyPlayer lobbyPlayer;

    private void OnEnable()
    {
        GameEvents.Lemmings.LemmingReachedExit += LemmingExit;
        GameEvents.Lemmings.LemmingSpawned += LemmingEnter;
        GameEvents.Lemmings.LemmingDied += LemmingDie;
        GameEvents.GameState.OnStartGame += OnStartGame;
        GameEvents.GameState.OnEndGame += OnEndGame;
        GameEvents.GameState.OnLoadGame += OnLoadGame;
        GameEvents.Lemmings.nukedLemmings += OnTeamNuke;
    }

    private void OnDisable()
    {
        GameEvents.Lemmings.LemmingReachedExit -= LemmingExit;
        GameEvents.Lemmings.LemmingSpawned -= LemmingEnter;
        GameEvents.Lemmings.LemmingDied -= LemmingDie;
        GameEvents.GameState.OnStartGame -= OnStartGame;
        GameEvents.GameState.OnEndGame -= OnEndGame;
        GameEvents.GameState.OnLoadGame -= OnLoadGame;
        GameEvents.Lemmings.nukedLemmings -= OnTeamNuke;
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
        lobbyPlayer = GetComponent<LNetworkLobbyPlayer>();
    }

    private void Update()
    {
        // server check winning and lose conditions
        if (isServer)
        {
            if (inGame)
            {
                remainingTime -= Time.deltaTime;

                CheckFinal();
            }
        }
    }

    private void CheckFinal()
    {
        // check timer
        if (remainingTime < 0)
        {
            LNetworkPlayer.Player1Instance.CmdInformExplodeAllLemmings();
            LNetworkPlayer.Player2Instance.CmdInformExplodeAllLemmings();
        }

        // check lemings on final
        int teamsFinished = 0;
        foreach (var key in lemmingsSpawned.Keys)
        {
            if (lemmingsEnteredExit[key] + lemmingsDied[key] >= LevelController.Instance.CurrentMapSettings.LemmingsCount)
            {
                teamsFinished++;
            }
        }

        if (teamsFinished >= lemmingsSpawned.Keys.Count)
        {
            CalculateEndLevel();
        }
    }

    private void CalculateEndLevel()
    {
        bool p1ReachedMin, p2reachedMin;
        Player victorious;

        p1ReachedMin = lemmingsEnteredExit[Player.Player1] > LevelController.Instance.CurrentMapSettings.MinimumVictoryCount;
        p2reachedMin = lemmingsEnteredExit[Player.Player2] > LevelController.Instance.CurrentMapSettings.MinimumVictoryCount;

        if (lemmingsEnteredExit[Player.Player1] > lemmingsEnteredExit[Player.Player2]) victorious = Player.Player1;
        else if (lemmingsEnteredExit[Player.Player1] < lemmingsEnteredExit[Player.Player2]) victorious = Player.Player2;
        else victorious = Player.None;

        RpcEndLevel(p1ReachedMin, p2reachedMin, victorious);
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
        Debug.Log("triggered local lemming exit");
        if (isServer)
        {
            Debug.Log("server triggered local lemming exit");
            RpcLemmingExit(lemming.GetComponent<NetworkIdentity>());
        }
    }

    private void LemmingEnter(LemmingStateController lemming)
    {
        if (isServer) RpcLemmingEnter(lemming.GetComponent<NetworkIdentity>());
    }


    private void LemmingDie(LemmingStateController lemming)
    {
        if (isServer)
        {
            RpcLemmingDie(lemming.GetComponent<NetworkIdentity>());
        }
    }

    private void OnTeamNuke(Player player)
    {
        if(isServer)
        {
            lemmingsDied[player] = LevelController.Instance.CurrentMapSettings.LemmingsCount - lemmingsEnteredExit[player] - lemmingsOnScene[player].Count;
        }
    }

    [ClientRpc]
    private void RpcLemmingExit(NetworkIdentity lemmingID)
    {
        if (lobbyPlayer.playerNum != LevelController.Instance.team) return;

        LemmingStateController lemming_ = lemmingID.GetComponent<LemmingStateController>();
        if (!lemmingsOnScene.ContainsKey(lemming_.Team)) lemmingsOnScene.Add(lemming_.Team, new List<LemmingStateController>());
        lemmingsOnScene[lemming_.Team].Remove(lemming_);
        lemmingsEnteredExit[lemming_.Team]++;
        GameEvents.NetworkLemmings.LemmingReachedExit.SafeInvoke(lemming_);
    }

    [ClientRpc]
    private void RpcLemmingEnter(NetworkIdentity lemmingID)
    {
        if (lobbyPlayer.playerNum != LevelController.Instance.team) return;
        LemmingStateController lemming_ = lemmingID.GetComponent<LemmingStateController>();
        lemmingsOnScene[lemming_.Team].Add(lemming_);
        lemmingsSpawned[lemming_.Team]++;
        GameEvents.NetworkLemmings.LemmingSpawned.SafeInvoke(lemming_);

    }

    [ClientRpc]
    private void RpcLemmingDie(NetworkIdentity lemmingID)
    {
        if (lobbyPlayer.playerNum != LevelController.Instance.team) return;
        LemmingStateController lemming_ = lemmingID.GetComponent<LemmingStateController>();
        lemmingsOnScene[lemming_.Team].Remove(lemming_);
        lemmingsDied[lemming_.Team]++;
        GameEvents.NetworkLemmings.LemmingDied.SafeInvoke(lemming_);
        lemmingID.GetComponent<LemmingActions>().EliminateLemming();

    }

    [ClientRpc]
    private void RpcPlayerWin(Player player)
    {
        if (lobbyPlayer.playerNum != LevelController.Instance.team) return;
        GameEvents.GameState.OnEndGame.SafeInvoke();
        GameEvents.GameState.OnPlayerWin.SafeInvoke(player);

    }

    [ClientRpc]
    private void RpcBothPlayersLose()
    {
        if (lobbyPlayer.playerNum != LevelController.Instance.team) return;
        GameEvents.GameState.OnEndGame.SafeInvoke();
        GameEvents.GameState.OnBothPlayersLose.SafeInvoke();

    }

    [ClientRpc]
    private void RpcTimeEnded()
    {
        if (lobbyPlayer.playerNum != LevelController.Instance.team) return;
        GameEvents.GameState.OnEndGame.SafeInvoke();
        GameEvents.GameState.OnBothPlayersLose.SafeInvoke();

    }

    [ClientRpc]
    private void RpcEndLevel(bool p1ReachedMinimum, bool p2ReachedMinimum, Player winPlayer)
    {
        GameEvents.GameState.OnEndGame.SafeInvoke();

        if (p1ReachedMinimum && p2ReachedMinimum)
        {
            GameEvents.GameState.OnBothPlayersWin.SafeInvoke(winPlayer);
        }
        else if (p1ReachedMinimum && !p2ReachedMinimum)
        {
            GameEvents.GameState.OnPlayerWin.SafeInvoke(Player.Player1);
        }
        else if (p2ReachedMinimum && !p1ReachedMinimum)
        {
            GameEvents.GameState.OnPlayerWin.SafeInvoke(Player.Player2);
        }
        else
        {
            GameEvents.GameState.OnBothPlayersLose.SafeInvoke();
        }

    }

}


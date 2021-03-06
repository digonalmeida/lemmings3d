﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LNetworkPlayer : NetworkBehaviour
{
    //Variables
    [SyncVar]
    public Player playerNum;
    [SyncVar]
    public bool levelSelectReady = false;
    [SyncVar]
    public int selectedLevel = -1;
    [SyncVar]
    public bool levelLoadReady = false;
    [SyncVar]
    public bool rematchReady = false;
    [SyncVar]
    public int spawnRateIndex = 1;

    public static LNetworkPlayer LocalInstance { get; private set; }
    public static LNetworkPlayer Player1Instance { get; private set; }
    public static LNetworkPlayer Player2Instance { get; private set; }


    private void Awake()
    {
        GameEvents.Map.OnMapLoaded += OnMapLoaded;
    }

    private void OnDestroy()
    {
        GameEvents.Map.OnMapLoaded -= OnMapLoaded;
    }

    private void resetVariables()
    {
        levelSelectReady = false;
        levelLoadReady = false;
        rematchReady = false;
        selectedLevel = -1;
    }

    //Start
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (isServer)
        {
            if (isLocalPlayer)
            {
                LocalInstance = this;
                Player1Instance = this;
                this.name = name + "_Player1";
            }
            else
            {
                Player2Instance = this;
                this.name = name + "_Player2";
            }
        }
        else
        {
            if (isLocalPlayer)
            {
                LocalInstance = this;
                Player2Instance = this;
                this.name = name + "_Player2";
            }
            else
            {
                Player1Instance = this;
                this.name = name + "_Player1";
            }
        }
    }

    //Start
    public override void OnStartAuthority()
    {
        //Base Method
        base.OnStartAuthority();

        //Set Player
        if (isServer) playerNum = Player.Player1;
        else playerNum = Player.Player2;

        //Inform Player Num
        CmdInformPlayerNum(playerNum);
    }

    public static LNetworkPlayer GetInstanceByTeam(Player team)
    {
        switch (team)
        {
            case Player.Player1:
            return Player1Instance;
            case Player.Player2:
            return Player2Instance;
            default:
            return null;
        }
    }


    [Command]
    public void CmdGiveSkill(NetworkIdentity lemming, Skill skill, Vector3 originPosition)
    {
        if (lemming == null)
        {
            Debug.LogError("Trying to give skill on a non lemming object");
            return;
        }
        else
        {
            lemming.GetComponent<LemmingStateController>().giveSkill(skill, originPosition);
        }
    }

    internal void SetSpawnRate(int currentSpawnRate)
    {
        CmdSetSpawnRate(currentSpawnRate);
    }

    [Command]
    private void CmdSetSpawnRate(int currentSpawnRate)
    {
        spawnRateIndex = currentSpawnRate;
        GameEvents.Lemmings.ChangedSpawnRate.SafeInvoke(playerNum);
    }

    [Command]
    public void CmdInformReadyStatus(bool ready)
    {
        levelSelectReady = ready;
    }

    [Command]
    public void CmdInformRematchReady(bool ready)
    {
        rematchReady = ready;
    }

    [Command]
    public void CmdInformExplodeAllLemmings()
    {
        if (playerNum == Player.Player1) Spawner.Player1Instance.StopAllCoroutines();
        else if (playerNum == Player.Player2) Spawner.Player2Instance.StopAllCoroutines();

        GameEvents.Lemmings.nukedLemmings.SafeInvoke(playerNum);
        SkillsController.Instance.executeExplodeAll(playerNum);
    }

    [Command]
    public void CmdResetVariables()
    {
        resetVariables();
    }

    [Command]
    public void CmdInformPlayerNum(Player playerNum)
    {
        this.playerNum = playerNum;
    }

    [Command]
    public void CmdSelectLevel(int indexButton)
    {
        this.selectedLevel = indexButton;
    }

    public void OnMapLoaded()
    {
        if (hasAuthority)
        {
            CmdSetMapLoaded();
        }
    }

    [Command]
    private void CmdSetMapLoaded()
    {
        levelLoadReady = true;
    }
}
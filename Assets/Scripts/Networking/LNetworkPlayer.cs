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
    public bool levelLoadReady = false;
    [SyncVar]
    public bool rematchReady = false;
    [SyncVar]
    public bool forceLemmingExplode = false;

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
        forceLemmingExplode = false;
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
    public void CmdInformExplodeAllLemmings(bool value)
    {
        forceLemmingExplode = value;
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
        LevelSelectorNetworkController.Instance.selectLevel(indexButton, playerNum);
    }

    [ClientRpc]
    public void RpcSelectLevel(int indexSelection, Player playerNum)
    {
        LevelSelectorPanelController.Instance.selectLevel(indexSelection, playerNum);
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
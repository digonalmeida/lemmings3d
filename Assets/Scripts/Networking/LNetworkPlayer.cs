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

    public void GiveSkill(LemmingStateController lemming, Skill skill)
    {
        if (!isServer && isLocalPlayer)
        {
            var netIdComponent = lemming.GetComponent<NetworkIdentity>();
            if(netIdComponent == null)
            {
                Debug.LogError("Can't give skill. lemming has no network identity");
                return;
            }

            var netId = netIdComponent.netId;
            CmdGiveSkill(netId, skill);
        }

        if(isServer)
        {
            lemming.giveSkill(skill);
        }
    }

    [Command]
    public void CmdGiveSkill(NetworkInstanceId lemmingNetId, Skill skill)
    {
        if (!isServer)
        {
            return;
        }

        var lemmingObject = NetworkServer.FindLocalObject(lemmingNetId);
        if(lemmingObject == null)
        {
            Debug.LogError("Local lemming object not found for id " + lemmingNetId.Value.ToString());
        }

        var lemming = lemmingObject.GetComponent<LemmingStateController>();
        if(lemming == null)
        {
            Debug.LogError("Trying to give skill on a non lemming object");
            return;
        }

        GiveSkill(lemming, skill);
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

    //Reset Rematch Ready
    [Command]
    public void CmdResetRematchReady()
    {
        rematchReady = false;
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
            CmdResetRematchReady();
            CmdSetMapLoaded();
        }
    }

    [Command]
    private void CmdSetMapLoaded()
    {
        levelLoadReady = true;
    }
}
using LevelMap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkGameFlow : NetworkBehaviour
{
    private enum GameFlowState
    {
        LevelSelect,
        LoadingLevel,
        inGame,
        ScorePanel
    };

    //References
    public GameObject UICanvasRef;
    public GameObject pointerObjectRef;
    public GameObject levelSelectorPanelRef;
    public GameObject scorePanelRef;

    //Network Player
    private LNetworkPlayer player1Ref;
    private LNetworkPlayer player2Ref;

    //Control Variables
    [SerializeField]
    private GameFlowState currentGameState;

    //Singleton
    private static NetworkGameFlow instance;
    public static NetworkGameFlow Instance
    {
        get
        {
            return instance;
        }
    }

    //On Object Awake
    private void Awake()
    {
        //Check Singleton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        GameEvents.GameState.OnLoadGame += OnLoadGame;
        GameEvents.GameState.OnEndGame += OnGameEnded;
    }

    private void OnDestroy()
    {
        GameEvents.GameState.OnLoadGame -= OnLoadGame;
        GameEvents.GameState.OnEndGame -= OnGameEnded;
    }

    //Start
    private void Start()
    {
        currentGameState = GameFlowState.LevelSelect;
    }

    public void OnLoadGame()
    {
        levelSelectorPanelRef.SetActive(false);
        UICanvasRef.SetActive(true);
        pointerObjectRef.SetActive(true);
        currentGameState = GameFlowState.LoadingLevel;
    }

    public void OnLevelLoaded()
    {
        if(!isServer)
        {
            return;
        }
        GameEvents.GameState.OnStartGame();
    }

    public void OnGameEnded()
    {
        currentGameState = GameFlowState.ScorePanel;
    }

    //Stop Game
    [ClientRpc]
    public void RpcEndGame()
    {
        LevelController.Instance.EndGame();
        scorePanelRef.SetActive(true);
    }

    bool CheckLevelLoaded()
    {
        var player1 = LNetworkPlayer.Player1Instance;
        if (player1 == null)
        {
            return false;
        }

        var player2 = LNetworkPlayer.Player2Instance;
        if (player2 == null)
        {
            return false;
        }

        return player1.levelLoadReady && player2.levelLoadReady;
    }

    bool CheckRematch()
    {
        var player1 = LNetworkPlayer.Player1Instance;
        if (player1 == null)
        {
            return false;
        }

        var player2 = LNetworkPlayer.Player2Instance;
        if (player2 == null)
        {
            return false;
        }

        return player1.rematchReady && player2.rematchReady;
    }

    bool CheckLevelSelected()
    {
        var player1 = LNetworkPlayer.Player1Instance;
        if (player1 == null)
        {
            return false;
        }

        var player2 = LNetworkPlayer.Player2Instance;
        if (player2 == null)
        {
            return false;
        }

        return player1.levelSelectReady && player2.levelSelectReady;
    }

    void LoadSelectedLevel()
    {
        int mapId = LevelSelectorNetworkController.Instance.getIndexMapAssetToLoad();
        if (!MapManager.Instance.TrySelectMapById(mapId))
        {
            Debug.LogErrorFormat("Couldn't select map with Id {0}", mapId);
            return;
        }

        currentGameState = GameFlowState.LoadingLevel;
        GameEvents.GameState.OnLoadGame.SafeInvoke();
    }

    public void StartGame()
    {
        GameEvents.GameState.OnStartGame.SafeInvoke();
        currentGameState = GameFlowState.inGame;
    }

    // Update is called once per frame
    void Update ()
    {
        if (!isServer)
        {
            return;
        }

        if (currentGameState == GameFlowState.LevelSelect)
        {
            if(CheckLevelSelected())
            {
                LoadSelectedLevel();
            }
        }

        if(currentGameState == GameFlowState.LoadingLevel)
        {
            if(CheckLevelLoaded())
            {
                StartGame();
            }
        }

        if (currentGameState == GameFlowState.ScorePanel)
        {
            if (CheckRematch())
            {
                LNetworkPlayer.Player1Instance.CmdResetVariables();
                LNetworkPlayer.Player2Instance.CmdResetVariables();
                LNetworkLobbyManager.singleton.ServerChangeScene("DefaultLevel");
            }
        }
    }
}

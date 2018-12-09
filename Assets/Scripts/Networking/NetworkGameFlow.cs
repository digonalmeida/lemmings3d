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
    }

    //Start
    private void Start()
    {
        currentGameState = GameFlowState.LevelSelect;
    }
	
    //End Game
    public void requestEndGame()
    {
        currentGameState = GameFlowState.ScorePanel;
        RpcEndGame();
    }

    //Load Level
    [ClientRpc]
    public void RpcLoadLevel(int indexMapAsset)
    {
        levelSelectorPanelRef.SetActive(false);
        UICanvasRef.SetActive(true);
        pointerObjectRef.SetActive(true);
        MapManager.Instance.SelectedMapAsset = MapManager.Instance.MapAssets[indexMapAsset];
        LevelController.Instance.LoadGame();
    }

    //Start Game
    [ClientRpc]
    public void RpcStartGame()
    {
        LevelController.Instance.StartGame();
    }

    //Stop Game
    [ClientRpc]
    public void RpcEndGame()
    {
        LevelController.Instance.EndGame();
        scorePanelRef.SetActive(true);
    }

    // Update is called once per frame
    void Update ()
    {
        if(isServer)
        {
            if (currentGameState == GameFlowState.LevelSelect)
            {
                if (LNetworkPlayer.Player1Instance != null && LNetworkPlayer.Player1Instance.levelSelectReady)
                {
                    if (LNetworkPlayer.Player2Instance != null && LNetworkPlayer.Player2Instance.levelSelectReady)
                    {
                        RpcLoadLevel(LevelSelectorNetworkController.Instance.getIndexMapAssetToLoad());
                        RpcStartGame();
                        currentGameState = GameFlowState.inGame;
                    }
                }
            }
        }
	}
}

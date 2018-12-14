using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using LevelMap;
using System;

public class NetworkGameStateEventsDispatcher : NetworkBehaviour
{
    private void Awake()
    {
        GameEvents.GameState.OnLoadGame += OnLoadGame;
        GameEvents.GameState.OnStartGame += OnStartGame;
        GameEvents.Lemmings.ChangedSpawnRate += ChangedSpawnRate;
    }

    private void OnDestroy()
    {
        GameEvents.GameState.OnLoadGame -= OnLoadGame;
        GameEvents.GameState.OnStartGame -= OnStartGame;
        GameEvents.Lemmings.ChangedSpawnRate -= ChangedSpawnRate;
    }

    private void OnLoadGame()
    {
        if (!isServer)
        {
            return;
        }

        var mapIndex = MapManager.Instance.SelectedMapIndex;
        if (mapIndex < 0)
        {
            Debug.LogError("Invalid map selected. Check if map asset is in the right directory");
            return;
        }

        RpcLoadGame(mapIndex);
    }

    private void OnStartGame()
    {
        if (!isServer)
        {
            return;
        }

        RpcStartGame();
    }


    private void ChangedSpawnRate(Player player)
    {
        if (!isServer)
        {
            return;
        }

        RpcChangedSpawnRate(player);
    }

    [ClientRpc]
    private void RpcLoadGame(int mapIndex)
    {
        if (isServer)
        {
            return;
        }

        bool ok = MapManager.Instance.TrySelectMapById(mapIndex);
        if (!ok)
        {
            Debug.LogError("Couldn't select map with index " + mapIndex.ToString() +
                ". Make sure asset is in the right directory");
        }
        GameEvents.GameState.OnLoadGame.SafeInvoke();
    }

    [ClientRpc]
    private void RpcStartGame()
    {
        if (isServer)
        {
            return;
        }

        GameEvents.GameState.OnStartGame.SafeInvoke();
    }

    [ClientRpc]
    private void RpcChangedSpawnRate(Player player)
    {
        if (isServer)
        {
            return;
        }

        GameEvents.Lemmings.ChangedSpawnRate.SafeInvoke(player);
    }
}

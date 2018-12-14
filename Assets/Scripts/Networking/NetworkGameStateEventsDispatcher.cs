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
        GameEvents.Map.OnAddBlock += OnAddBlock;
        GameEvents.Map.OnRemoveBlock += OnRemoveBlock;
    }

    private void OnDestroy()
    {
        GameEvents.GameState.OnLoadGame -= OnLoadGame;
        GameEvents.GameState.OnStartGame -= OnStartGame;
        GameEvents.Lemmings.ChangedSpawnRate -= ChangedSpawnRate;
        GameEvents.Map.OnAddBlock -= OnAddBlock;
        GameEvents.Map.OnRemoveBlock -= OnRemoveBlock;
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

    public void OnAddBlock(Vector3Int position, MapBlock b)
    {
        if (!isServer)
        {
            return;
        }
        b.Position = position;
        Debug.Log("sending add block");
        RpcAddBlock(position.x, position.y, position.z, b.Type, b.Direction);
    }

    public void OnRemoveBlock(Vector3Int position)
    {
        if(!isServer)
        {
            return;
        }
        Debug.Log("sending remove block");
        RpcRemoveBlock(position.x, position.y, position.z);
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
    


    [ClientRpc]
    public void RpcAddBlock(int x, int y, int z, MapBlock.BlockType type, Direction direction)
    {
        if(isServer)
        {
            return;
        }

        var pos = new Vector3Int(x, y, z);
        var block = new MapBlock();
        block.Type = type;
        block.Direction = direction;
        MapController.Instance.AddBlock(pos, block);
    }

    [ClientRpc]
    public void RpcRemoveBlock(int x, int y, int z)
    {
        if (isServer)
        {
            return;
        }
        MapController.Instance.EraseBlock(new Vector3Int(x, y, z));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using LevelMap;
using UnityEngine;

public class NetworkMapController : NetworkBehaviour
{
    private bool _initialized = false;

    [SerializeField]
    private LevelMap.MapData map;

    private void Initialize()
    {
        if(_initialized)
        {
            return;
        }

        _initialized = true;

        GameEvents.Map.OnMapLoaded += OnMapLoaded;
        GameEvents.Map.OnAddBlock += OnAddBlock;
        GameEvents.Map.OnRemoveBlock += OnRemoveBlock;
    }

    private void Dispatch()
    {
        if(!_initialized)
        {
            return;
        }
        _initialized = false;

        GameEvents.Map.OnMapLoaded -= OnMapLoaded;
        GameEvents.Map.OnAddBlock -= OnAddBlock;
        GameEvents.Map.OnRemoveBlock -= OnRemoveBlock;

        StopAllCoroutines();
    }

    private void OnMapLoaded()
    {
        if(!isServer)
        {
            return;
        }

        map = MapController.Instance.Map;
        Debug.Log(map.Blocks.Count + " blocks");
        var jsonMap = JsonUtility.ToJson(map);
        RpcUpdateMap(jsonMap);
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

    [ClientRpc]
    private void RpcUpdateMap(string jsonMapData)
    {
        if(isServer)
        {
            return;
        }

        var data = JsonUtility.FromJson<MapData>(jsonMapData);

        Debug.Log(data.Blocks.Count + " blocks");
        map = data;
        MapController.Instance.Map = map;
        MapController.Instance.RefreshScene();
    }

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        Initialize();
    }

    private void OnDisable()
    {
        Dispatch();
    }

    private void OnDestroy()
    {
        Dispatch();
    }
}

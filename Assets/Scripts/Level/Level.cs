using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level: ISerializationCallbackReceiver
{
    //Used to save level on serialization
    [HideInInspector]
    [SerializeField]
    private List<LevelBlock> serializedLevelBlocks = new List<LevelBlock>();

    private Dictionary<Vector3Int, LevelBlock> blocks = new Dictionary<Vector3Int, LevelBlock>();

    public Dictionary<Vector3Int, LevelBlock> Blocks
    {
        get
        {
            return blocks;
        }
    }

    public void Clear()
    {
        blocks.Clear();
    }
    

    public void Set(Vector3Int position, LevelBlock levelBlock)
    {
        blocks[position] = levelBlock;
    }

    public void Rotate(Vector3Int position)
    {
        var block = blocks[position];
        if(block == null)
        {
            return;
        }
        block.Rotate();
    }

    public LevelBlock Get(Vector3Int position)
    {
        if(!blocks.ContainsKey(position))
        {
            return null;
        }

        return blocks[position];
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        serializedLevelBlocks.Clear();

        foreach(var pair in blocks)
        {
            var pos = pair.Key;
            var block = pair.Value;
            if (block == null)
            {
                continue;
            }
            block.Position = pos;
            serializedLevelBlocks.Add(block);
        }
    }

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        blocks.Clear();
        foreach(var block in serializedLevelBlocks)
        {
            blocks.Add(block.Position, block);
        }
    }
}

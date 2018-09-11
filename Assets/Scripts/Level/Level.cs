using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "level", menuName = "lemmings/level", order = 1)]
[System.Serializable]
public class Level: ScriptableObject
{
    [SerializeField]
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

    public LevelBlock Get(Vector3Int position)
    {
        if(!blocks.ContainsKey(position))
        {
            return null;
        }

        return blocks[position];
    }
}

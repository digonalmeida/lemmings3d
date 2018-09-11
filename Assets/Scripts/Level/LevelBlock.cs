using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelBlock
{
    [SerializeField]
    private BlockType type = BlockType.Simple;

    [SerializeField]
    private Directions direction = Directions.North;

    public enum BlockType
    {
        Empty,
        Simple,
        Stairs
    }
    
    public enum Directions
    {
        North,
        East,
        South,
        West
    }

    public BlockType Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }

    public Directions Direction
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value;
        }
    }
}

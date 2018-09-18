using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelBlock
{
    /*The position is used on level serialization*/
    [HideInInspector]
    [SerializeField]
    private Vector3Int position;

    [SerializeField]
    private BlockType type = BlockType.Simple;

    [SerializeField]
    private Directions direction = Directions.North;

    public void Rotate()
    {
        direction = (Directions)(((int)direction + 1) % 4);
    }

    public enum BlockType
    {
        Empty,
        Simple,
        Stairs,
        Start,
        End
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

    public Vector3Int Position
    {
        get
        {
            return position;
        }
        set
        {
            position = value;
        }

    }
}

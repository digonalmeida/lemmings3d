using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    North,
    East,
    South,
    West,
    Up,
    Down,
    None
}

public static class Directions
{
    public static Vector3 North = Vector3.forward;
    public static Vector3 East = Vector3.right;
    public static Vector3 South = Vector3.back;
    public static Vector3 West = Vector3.left;
    public static Vector3 Up = Vector3.up;
    public static Vector3 Down = Vector3.down;
    public static Vector3 None = Vector3.zero;

    public static Vector3 GetWorldDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                return Vector3.forward;
            case Direction.East:
                return Vector3.right;
            case Direction.South:
                return Vector3.back;
            case Direction.West:
                return Vector3.left;
            case Direction.Up:
                return Up;
            case Direction.Down:
                return Down;
            default:
                return Vector3.zero;
        }
    }

    public static Direction getOppositeDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                return Direction.South;
            case Direction.East:
                return Direction.West;
            case Direction.South:
                return Direction.North;
            case Direction.West:
                return Direction.East;
            case Direction.Up:
                return Direction.Down;
            case Direction.Down:
                return Direction.Up;
            case Direction.None:
                return Direction.None;
            default:
                return Direction.None;
        }
    }
}

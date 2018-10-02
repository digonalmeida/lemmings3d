using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    North,
    East,
    South,
    West,
}


public static class Directions
{
    public static Vector3 North = Vector3.forward;
    public static Vector3 East = Vector3.right;
    public static Vector3 South = Vector3.back;
    public static Vector3 West = Vector3.left;

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
            default:
                return Vector3.zero;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "level", menuName = "lemmings/level", order = 1)]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private Level level = new Level();

    public Level Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }
}

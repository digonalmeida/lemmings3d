using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "level", menuName = "lemmings/level", order = 1)]
[System.Serializable]

public class LevelBlockController : MonoBehaviour
{
    [SerializeField]
    private GameObject destroyEffectPrefab = null;

    [SerializeField]
    LevelBlock levelBlock = null;

    public LevelBlock Block
    {
        get
        {
            return levelBlock;
        }
        set
        {
            levelBlock = value;
        }
    }

    private void OnValidate()
    {
        UpdateBlockState();
    }

    public void DestroyBlock()
    {
        Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    void Update()
    {
        UpdateBlockState();
    }
    public void UpdateBlockState()
    {
        if(levelBlock == null)
        {
            return;
        }

        float rot = 0;
        switch (levelBlock.Direction)
        {
            case LevelBlock.Directions.North:
                rot = 0;
                break;
            case LevelBlock.Directions.East:
                rot = 90;
                break;
            case LevelBlock.Directions.South:
                rot = 180;
                break;
            case LevelBlock.Directions.West:
                rot = 270;
                break;
            default:
                break;
        }

        var euler = transform.eulerAngles;
        euler.y = rot;
        transform.eulerAngles = euler;
    }
}

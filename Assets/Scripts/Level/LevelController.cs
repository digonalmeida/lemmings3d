using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    [SerializeField]
    private Level level;

    [SerializeField]
    private List<LevelBlockController> blockPrefabs = new List<LevelBlockController>();

    [SerializeField]
    private Vector3Int inputPosition = new Vector3Int(0,0,0);

    private float timeout = 0.1f;

    [SerializeField]
    private Dictionary<Vector3Int, LevelBlockController> levelBlocks = new Dictionary<Vector3Int, LevelBlockController>();

    [SerializeField]
    private LevelBlock brushBlock = new LevelBlock();

    [SerializeField]
    private LevelBlock.BlockType inputBlock = LevelBlock.BlockType.Simple;

    [SerializeField]
    private List<LevelData> levels;

    [SerializeField]
    private int selectedLevelSlot = 0;

    private Level lastLevel = null;

    public void LoadLevel()
    {
        level = levels[selectedLevelSlot].Level;
        UpdateLevel();
        Refresh();
    }

    public void SaveLevel()
    {
        levels[selectedLevelSlot].Level = level;
    }

    public void Refresh()
    {
        ClearLevelBlocks();
        UpdateLevel();
    }

    public void Clear()
    {
        level.Clear();
        ClearLevelBlocks();
        UpdateLevel();
    }

    public void ClearLevelBlocks()
    {
        foreach(var pair in levelBlocks)
        {
            RemoveLevelBlock(pair.Key);
        }
        levelBlocks.Clear();
    }

    public void AddBlock(Vector3Int position)
    {
        var block = new LevelBlock();
        block.Type = brushBlock.Type;
        block.Direction = brushBlock.Direction;
        block.Position = brushBlock.Position;
        level.Set(position, block);
        UpdateLevel();
    }

    public void SetBlockEmpty(Vector3Int position)
    {
        level.Set(position, null);
        UpdateLevel();
    }

    public void Rotate(Vector3Int position)
    {
        level.Rotate(position);
        UpdateLevel();
    }

    public void SetInputBlock()
    {
        var block = new LevelBlock();
        block.Type = brushBlock.Type;
        block.Direction = brushBlock.Direction;
        block.Position = brushBlock.Position;
        level.Set(inputPosition, block);
        UpdateLevel();
    }

    public void UpdateLevel()
    {
        var blocks = level.Blocks;
        foreach(var pair in blocks)
        {
            SpawnLevelBlock(pair.Key, pair.Value);
        }
    }

    public void ChangeType()
    {
        brushBlock.Type = (LevelBlock.BlockType)(Mathf.Max(1, (((int)brushBlock.Type + 1) % 5)));
    }
    public void SpawnLevelBlock(Vector3Int position, LevelBlock levelBlock)
    {
        var block = GetLevelBlock(position);

        if(block != null)
        {
            if (block.Block != levelBlock)
            {
                RemoveLevelBlock(position);
            }
            else
            {
                return;
            }
        }
        else
        {
            RemoveLevelBlock(position);
        }

        LevelBlockController prefab = null;
        block = null;

        if(levelBlock != null)
        {
            prefab = FindblockPrefabWithType(levelBlock.Type);
        }

        if(prefab != null)
        {
            block = Instantiate(prefab).GetComponent<LevelBlockController>();
            block.transform.position = position;
            block.transform.parent = transform;
            block.Block = levelBlock;
            block.UpdateBlockState();
        }
        
        levelBlocks[position] = block;
    }

    private void RemoveLevelBlock(Vector3Int position)
    {
        var block = GetLevelBlock(position);
        if (block != null)
        {
            if (!Application.isPlaying)
            {
                DestroyImmediate(block.gameObject);
            }
            else
            {
                block.DestroyBlock();
            }
            
        }
    }

    public LevelBlockController GetLevelBlock(Vector3Int position)
    {
        if (!levelBlocks.ContainsKey(position))
        {
            return null;
        }
        return levelBlocks[position];
    }

    public void LoadFromScene()
    {
        var children = GetComponentsInChildren<LevelBlockController>();
        levelBlocks.Clear();

        foreach(var child in children)
        {
            levelBlocks[Vector3Int.RoundToInt(child.transform.position)] = child;
        }
    }

    private LevelBlockController FindblockPrefabWithType(LevelBlock.BlockType type)
    {
        foreach(var block in blockPrefabs)
        {
            if(block == null)
            {
                continue;
            }

            if(block.Block.Type == type)
            {
                return block;
            }
        }
        return null;
    }

    public void Update()
    {
    }
}

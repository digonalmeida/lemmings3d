using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    [SerializeField]
    private Level level;

    private Level lastLevel = null;

    [SerializeField]
    private GameObject cursor;

    [SerializeField]
    private GameObject addCursor;

    [SerializeField]
    private LevelBlockController emptyPrefab;

    [SerializeField]
    private LevelBlockController simplePrefab;

    [SerializeField]
    private LevelBlockController stairsPrefab;

    [SerializeField]
    private Vector3Int inputPosition = new Vector3Int(0,0,0);

    private float timeout = 0.1f;

    [SerializeField]
    private Dictionary<Vector3Int, LevelBlockController> levelBlocks = new Dictionary<Vector3Int, LevelBlockController>();

    [SerializeField]
    private LevelBlock.BlockType brushBlock = LevelBlock.BlockType.Simple;

    [SerializeField]
    private LevelBlock.BlockType inputBlock = LevelBlock.BlockType.Simple;

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

    public void SetInputBlock()
    {
        var block = new LevelBlock();
        block.Type = inputBlock;
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

        LevelBlockController prefab = null;
        if(levelBlock != null)
        {
            switch (levelBlock.Type)
            {
                case LevelBlock.BlockType.Empty:
                    prefab = emptyPrefab;
                    break;
                case LevelBlock.BlockType.Simple:
                    prefab = simplePrefab;
                    break;
                case LevelBlock.BlockType.Stairs:
                    prefab = stairsPrefab;
                    break;
                default:
                    break;
            }

        }

        block = null;

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

    public void RemoveLevelBlock(Vector3Int position)
    {
        var block = GetLevelBlock(position);
        if (block != null)
        {
            block.DestroyBlock();
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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            brushBlock = LevelBlock.BlockType.Simple;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            brushBlock = LevelBlock.BlockType.Stairs;
        }

        if (GetSelectMousePosition(out inputPosition))
        {
            cursor.transform.position = inputPosition;
        }
        else
        {
            return;
        }

        if (GetAddMousePosition(out inputPosition))
        {
            addCursor.transform.position = inputPosition;
        }
      
        timeout -= Time.deltaTime;
        if(timeout<=0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetMouseButton(0))
                {
                    inputPosition = Vector3Int.RoundToInt(cursor.transform.position);
                    inputBlock = LevelBlock.BlockType.Empty;
                    level.Set(inputPosition, null);
                    UpdateLevel();
                    timeout = 0.1f;
                }
            }
            else if (Input.GetMouseButton(0))
            {
                inputPosition = Vector3Int.RoundToInt(addCursor.transform.position);
                inputBlock = brushBlock;
                SetInputBlock();
                timeout = 0.1f;
            }
        }
    }

    public bool GetAddMousePosition(out Vector3Int position)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 1000, LayerMask.GetMask("Wall")))
        {
            var distanceVector = hitInfo.point - hitInfo.collider.transform.position;
            if(Mathf.Abs(distanceVector.x) > Mathf.Abs(distanceVector.y))
            {
                if(Mathf.Abs(distanceVector.x) > Mathf.Abs(distanceVector.z))
                {
                    distanceVector.y = 0;
                    distanceVector.z = 0;
                }
                else
                {
                    distanceVector.x = 0;
                    distanceVector.y = 0;
                }
                
            }
            else
            {
                if (Mathf.Abs(distanceVector.y) > Mathf.Abs(distanceVector.z))
                {
                    distanceVector.x = 0;
                    distanceVector.z = 0;
                }
                else
                {
                    distanceVector.y = 0;
                    distanceVector.z = 0;
                }
            }
            distanceVector.Normalize();
            position = Vector3Int.RoundToInt(hitInfo.point + (distanceVector/2));

            return true;
        }
        position = new Vector3Int();
        return false;
    }
    public bool GetSelectMousePosition(out Vector3Int position)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 1000, LayerMask.GetMask("Wall")))
        {

            position = Vector3Int.RoundToInt(hitInfo.collider.transform.position);

            return true;
        }
        position = new Vector3Int();
        return false;
    }
}

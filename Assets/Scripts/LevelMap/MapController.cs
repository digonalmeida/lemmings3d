﻿namespace LevelMap
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MapController : MonoBehaviour
    {
        [SerializeField]
        private MapData map;

        [SerializeField]
        private Dictionary<Vector3Int, MapBlockController> levelBlocks = new Dictionary<Vector3Int, MapBlockController>();

        [SerializeField]
        private MapAsset mapAsset;

        public void LoadLevel()
        {
            LoadFromScene();
            map = mapAsset.LevelMap;
            RefreshScene();
        }

        public void SaveLevel()
        {
            LoadFromScene();
            mapAsset.LevelMap = map;
            RefreshScene();            
        }

        public void RefreshScene()
        {
            ClearLevelBlocks();
            BuildMapScene();
        }

        public void Clear()
        {
            map.Clear();
            ClearLevelBlocks();
            BuildMapScene();
        }

        public void ClearLevelBlocks()
        {
            foreach (var pair in levelBlocks)
            {
                RemoveSceneBlock(pair.Key);
            }

            levelBlocks.Clear();
        }

        public void AddBlock(Vector3Int position, MapBlock blockModel)
        {
            map.Set(position, new MapBlock(blockModel));
            BuildMapScene();
        }

        public void EraseBlock(Vector3Int position)
        {
            map.Set(position, null);
            BuildMapScene();
        }

        public void Rotate(Vector3Int position)
        {
            map.Rotate(position);
            BuildMapScene();
        }

        private void BuildMapScene()
        {
            var blocks = map.Blocks;
            foreach (var pair in blocks)
            {
                SpawnSceneBlock(pair.Key, pair.Value);
            }
        }

        private void Start()
        {
            LoadFromScene();
            BuildMapScene();
        }

        private void SpawnSceneBlock(Vector3Int position, MapBlock levelBlock)
        {
            var block = GetSceneBlock(position);
            if (block != null)
            {
                if(block.Block == levelBlock)
                {
                    return;
                }
            }
            
            RemoveSceneBlock(position);
            block = MapManager.Instance.InstantiateSceneBlock(levelBlock);
            if(block != null)
            {
                block.transform.position = position;
                block.transform.parent = transform;
                block.UpdateBlockState();
            }

            levelBlocks[position] = block;
        }

        private void RemoveSceneBlock(Vector3Int position)
        {
            var block = GetSceneBlock(position);
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

        private MapBlockController GetSceneBlock(Vector3Int position)
        {
            if (!levelBlocks.ContainsKey(position))
            {
                return null;
            }

            return levelBlocks[position];
        }

        private void LoadFromScene()
        {
            var children = GetComponentsInChildren<MapBlockController>();
            map.Clear();
            levelBlocks.Clear();
            foreach (var child in children)
            {
                var position = Vector3Int.RoundToInt(child.transform.position);

                levelBlocks[position] = child;
                map.Set(position, child.Block);
            }
        }

        private MapBlockController FindBlockPrefabWithType(MapBlock.BlockType type)
        {
            return MapManager.Instance.FindBlockPrefabWithType(type);
        }
    }
}
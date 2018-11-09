﻿namespace LevelMap
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MapController : Singleton
    {
        [SerializeField]
        private float spawnPropChance = 0.1f;

        [SerializeField]
        private MapData map = new MapData();

        [SerializeField]
        private MapSettings settings = new MapSettings();

        [SerializeField]
        private GameObject mapEditor;

        private Dictionary<Vector3Int, MapBlockController> levelBlocks = new Dictionary<Vector3Int, MapBlockController>();

        private GameObject blocksParent = null;

        public GameObject BlocksParent
        {
            get
            {
                if (blocksParent == null)
                {
                    blocksParent = GameObject.Find("_LevelBlocks");
                }

                if (blocksParent == null)
                {
                    blocksParent = new GameObject("_LevelBlocks");
                }

                return blocksParent;
            }
        }

        public static event Action<MapSettings> OnLoadMap;

        public void ToggleMapEditor()
        {
            mapEditor.SetActive(!mapEditor.activeInHierarchy);
        }

        public void LoadLevel()
        {
            LoadFromScene();
            map =  MapManager.Instance.SelectedMapAsset.LevelMap;
            settings = MapManager.Instance.SelectedMapAsset.Settings;
            RefreshScene();
        }

        public void SaveLevel()
        {
            LoadFromScene();
            MapManager.Instance.SelectedMapAsset.Settings = settings;
            MapManager.Instance.SelectedMapAsset.LevelMap = map;
            RefreshScene();            
        }

        public void RefreshScene()
        {
            ClearLevelBlocks();
            BuildMapScene();
            if (OnLoadMap != null)
            {
                OnLoadMap(settings);
            }
        }

        public void Clear()
        {
            map.Clear();
            RefreshScene();
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

        public void EraseWall(Vector3Int position)
        {
            LevelMap.MapBlock block = map.Get(position);
            if (block != null && block.Type == MapBlock.BlockType.Simple)
            {
                map.Set(position, null);
                BuildMapScene();
            }
        }

        public void Rotate(Vector3Int position)
        {
            map.Rotate(position);
            BuildMapScene();
        }

        public void SpawnProps()
        {
            var props = GameObject.FindObjectsOfType<RandomPropSpawner>();
            for (var i = 0; i < props.Length; i++)
            {
                props[i].TrySpawnProp(spawnPropChance);
            }
        }

        private void Awake()
        {
            GameEvents.GameState.OnLoadGame += LoadGameMap;
        }

        private void OnDestroy()
        {

            GameEvents.GameState.OnLoadGame -= LoadGameMap;
        }

        public void LoadGameMap()
        {
            LoadLevel();
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
            if (!GameManager.Instance.LoadAssetsOnLoad)
            {
                LoadFromScene();
                BuildMapScene();
            }
            
            if (OnLoadMap != null)
            {
                OnLoadMap(settings);
            }
            SpawnProps();
        }

        private void SpawnSceneBlock(Vector3Int position, MapBlock levelBlock)
        {
            var block = GetSceneBlock(position);
            if (block != null)
            {
                block.UpdateBlockState();
                if (block.Block == levelBlock)
                {
                    return;
                }
            }
            
            RemoveSceneBlock(position);
            block = MapManager.Instance.InstantiateSceneBlock(levelBlock);
            if (block != null)
            {
                block.transform.position = position;
                block.transform.parent = BlocksParent.transform;
                block.UpdateBlockState();
                block.SpawnProps(map.Get(new Vector3Int(position.x, position.y + 1, position.z)) == null ? true : false);
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
            var children = BlocksParent.GetComponentsInChildren<MapBlockController>();
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

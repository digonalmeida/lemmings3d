namespace LevelMap
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MapController : MonoBehaviour
    {
        [SerializeField]
        private MapAsset mapAsset;

        [SerializeField]
        private float spawnPropChance = 0.1f;

        [SerializeField]
        private MapData map = new MapData();

        [SerializeField]
        private MapSettings settings = new MapSettings();

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

        public void LoadLevel()
        {
            LoadFromScene();
            map =  mapAsset.LevelMap;
            settings = mapAsset.Settings;
            RefreshScene();
        }

        public void SaveLevel()
        {
            LoadFromScene();
            mapAsset.Settings = settings;
            mapAsset.LevelMap = map;
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
            LevelController.OnLoadGame += LoadGameMap;
        }

        private void OnDestroy()
        {
            LevelController.OnLoadGame -= LoadGameMap;
        }

        public void LoadGameMap()
        {
            mapAsset = GameManager.Instance.SelectedMapAsset;
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

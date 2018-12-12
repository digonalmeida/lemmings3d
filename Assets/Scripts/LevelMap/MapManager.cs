namespace LevelMap
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MapManager : MonoBehaviour
    {
        private static MapManager instance = null;
        private static string prefabName = "MapManager";

        [SerializeField]
        private List<MapBlockController> blockPrefabs = new List<MapBlockController>();

        [SerializeField]
        private MapAsset selectedMapAsset;

        [SerializeField]
        private List<MapAsset> mapAssets = new List<MapAsset>();
        
        public MapAsset SelectedMapAsset
        {
            get
            {
                return selectedMapAsset;
            }
            set
            {
                selectedMapAsset = value;
            }
        }

        public int SelectedMapIndex
        {
            get
            {
                return mapAssets.FindIndex((mapAsset) => selectedMapAsset == mapAsset);
            }
        }

        public bool TrySelectMapById(int id)
        {
            if (id < 0 || id > mapAssets.Count)
            {
                return false;
            }

            selectedMapAsset = mapAssets[id];
            return true;
        }

        public static MapManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<MapManager>();
                }

                if (instance == null)
                {
                    instance = CreateInstance();
                }

                return instance;
            }
        }

        public List<MapBlockController> BlockPrefabs
        {
            get
            {
                return blockPrefabs;
            }
        }

        public List<MapAsset> MapAssets
        {
            get
            {
                return mapAssets;
            }
        }
        
        public MapBlockController FindBlockPrefabWithType(MapBlock.BlockType type)
        {
            foreach (var block in blockPrefabs)
            {
                if (block == null)
                {
                    continue;
                }

                if (block.Block.Type == type)
                {
                    return block;
                }
            }

            return null;
        }

        public MapBlockController InstantiateSceneBlock(MapBlock mapBlock)
        {
            MapBlockController blockController = null;
            if (mapBlock == null)
            {
                return null;
            }

            var prefab = FindBlockPrefabWithType(mapBlock.Type);
            if (prefab == null)
            {
                return null;
            }

            blockController = Instantiate(prefab).GetComponent<MapBlockController>();
            blockController.Block = mapBlock;
            blockController.UpdateBlockState();
            return blockController;
        }

        private static MapManager CreateInstance()
        {
            var prefab = Resources.Load<GameObject>(prefabName);
            if (prefab == null)
            {
                Debug.LogError("No MapManager prefab found in Resources");
                return null;
            }

            var mapManagerObject = Instantiate(prefab);
            var mapManagerInstance = mapManagerObject.GetComponent<MapManager>();
            if (mapManagerInstance == null)
            {
                Debug.LogError("No MapManager component found in Map Manager");
                return null;
            }

            mapManagerObject.name = "_" + prefabName;
            mapManagerObject.transform.SetAsFirstSibling();
            return mapManagerInstance;
        }

        public void LoadMapAssets()
        {
            var mapAssetsArray = Resources.LoadAll<MapAsset>("maps/");
            mapAssets = new List<MapAsset>(mapAssetsArray);
        }

        public void OnValidate()
        {
            LoadMapAssets();
        }

        private void Awake()
        {
            var mapManagers = FindObjectsOfType<MapManager>();
            bool thereAreOthers = false;
            foreach (var mapManager in mapManagers)
            {
                if (mapManager == this)
                {
                    continue;
                }

                thereAreOthers = true;
            }

            if (thereAreOthers)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }

            LoadMapAssets();
        }
    }
}
namespace LevelMap
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class MapData : ISerializationCallbackReceiver
    {
        
        // Used to save level on serialization
        [SerializeField]
        private List<MapBlock> serializedLevelBlocks = new List<MapBlock>();

        private Dictionary<Vector3Int, MapBlock> blocks = new Dictionary<Vector3Int, MapBlock>();

        public MapData()
        {

        }

        public MapData(MapData other)
        {
            blocks = new Dictionary<Vector3Int, MapBlock>(other.blocks);

        }

        public Dictionary<Vector3Int, MapBlock> Blocks
        {
            get
            {
                return blocks;
            }
        }

        public void Clear()
        {
            blocks.Clear();
        }

        public void Set(Vector3Int position, MapBlock levelBlock)
        {
            blocks[position] = levelBlock;
        }

        public void Rotate(Vector3Int position)
        {
            var block = blocks[position];
            if (block == null)
            {
                return;
            }

            block.Rotate();
        }

        public void ChangeTeam(Vector3Int position)
        {
            MapBlock block = blocks[position];
            if (block == null)
            {
                return;
            }

            block.ChangeTeam();
        }
        public MapBlock Get(Vector3Int position)
        {
            if (!blocks.ContainsKey(position))
            {
                return null;
            }

            return blocks[position];
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            serializedLevelBlocks.Clear();

            foreach (var pair in blocks)
            {
                var pos = pair.Key;
                var block = pair.Value;
                if (block == null)
                {
                    continue;
                }

                block.Position = pos;
                serializedLevelBlocks.Add(block);
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            blocks.Clear();
            foreach (var block in serializedLevelBlocks)
            {
                blocks.Add(block.Position, block);
            }
        }
    }
}
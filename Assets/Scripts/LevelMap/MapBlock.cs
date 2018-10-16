namespace LevelMap
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class MapBlock
    {
        /*The position is used on level serialization*/
        [HideInInspector]
        [SerializeField]
        private Vector3Int position;

        [SerializeField]
        private BlockType type = BlockType.Simple;

        [SerializeField]
        private Direction direction = Direction.West;

        public MapBlock(MapBlock block)
        {
            position = block.position;
            direction = block.direction;
            type = block.type;
        }

        public MapBlock()
        {
        }

        public enum BlockType
        {
            Empty,
            Simple,
            Stairs,
            Start,
            End
        }

        public BlockType Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public Direction Direction
        {
            get
            {
                return direction;
            }

            set
            {
                direction = value;
            }
        }

        public Vector3Int Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public void Rotate()
        {
            direction = (Direction)(((int)direction + 1) % 4);
        }
    }
}
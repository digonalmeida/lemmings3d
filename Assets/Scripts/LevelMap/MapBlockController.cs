namespace LevelMap
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    [System.Serializable]
    public class MapBlockController : MonoBehaviour
    {
        [SerializeField]
        private GameObject destroyEffectPrefab = null;

        [SerializeField]
        private MapBlock block = null;

        public MapBlock Block
        {
            get
            {
                return block;
            }

            set
            {
                block = value;
            }
        }

        public void DestroyBlock()
        {
            Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        public void UpdateBlockState()
        {
            if (block == null)
            {
                return;
            }

            float rot = 0;
            switch (block.Direction)
            {
                case MapBlock.Directions.North:
                    rot = 0;
                    break;
                case MapBlock.Directions.East:
                    rot = 90;
                    break;
                case MapBlock.Directions.South:
                    rot = 180;
                    break;
                case MapBlock.Directions.West:
                    rot = 270;
                    break;
                default:
                    break;
            }

            var euler = transform.eulerAngles;
            euler.y = rot;
            transform.eulerAngles = euler;
        }

        private void OnValidate()
        {
            UpdateBlockState();
        }
        

       

        /*
        private void Update()
        {
            UpdateBlockState();
        }*/
    }
}
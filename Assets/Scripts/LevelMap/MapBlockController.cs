﻿namespace LevelMap
{
    using System;
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
                case Direction.North:
                    rot = 0;
                    break;
                case Direction.East:
                    rot = 90;
                    break;
                case Direction.South:
                    rot = 180;
                    break;
                case Direction.West:
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

        internal void SpawnProps(bool enabled)
        {
            RandomPropSpawner[] props = GetComponentsInChildren<RandomPropSpawner>();
            foreach (RandomPropSpawner prop in props)
            {
                prop.Init(enabled);
            }
        }




        /*
        private void Update()
        {
            UpdateBlockState();
        }*/
    }
}
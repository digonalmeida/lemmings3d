namespace LevelMap
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "Map", menuName = "lemmings/map", order = 1)]
    public class MapAsset : ScriptableObject
    {
        [SerializeField]
        private string mapName = "no name";

        [SerializeField]
        private MapData level = new MapData();

        public MapData LevelMap
        {
            get
            {
                return level;
            }

            set
            {
                level = value;
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
    }
}
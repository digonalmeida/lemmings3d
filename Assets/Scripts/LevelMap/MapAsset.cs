namespace LevelMap
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "Map", menuName = "lemmings/map", order = 1)]
    public class MapAsset : ScriptableObject
    {
#pragma warning disable 414 //assigned but never used
        [SerializeField]
        private string mapName = "no name";
#pragma warning restore 414

        [SerializeField]
        private MapSettings settings = new MapSettings();

        [SerializeField]
        private MapData level = new MapData();

        public MapData LevelMap
        {
            get
            {
                return new MapData(level);
            }

            set
            {
                level = new MapData(value);

//                UnityEditor.EditorUtility.SetDirty(this);
            }
        }

        public MapSettings Settings
        {
            get
            {
                return new MapSettings(settings);
            }
            set
            {
                settings = new MapSettings(value);
            }
        }
    }
}
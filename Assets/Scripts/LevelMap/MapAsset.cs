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
        public Sprite mapScrenshot;

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
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
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
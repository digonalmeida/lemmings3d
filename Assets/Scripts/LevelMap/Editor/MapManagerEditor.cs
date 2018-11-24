using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace LevelMap
{
    [CustomEditor(typeof(MapManager))]
    public class MapManagerEditor : Editor
    {
        int index = 0;
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var mapController = (MapManager)target;
            var assets = mapController.MapAssets;

            List<string> names = new List<string>();

            foreach(var asset in assets)
            {
                names.Add(asset.name);
            }
            index = mapController.SelectedMapIndex;
            index = EditorGUILayout.Popup("Selected Map", index, names.ToArray());
            mapController.TrySelectMapById(index);
        }
    }
}
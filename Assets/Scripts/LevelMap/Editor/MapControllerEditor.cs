using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace LevelMap
{
    [CustomEditor(typeof(MapController))]
    public class MapControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var levelController = (MapController)target;
            if (GUILayout.Button("Refresh"))
            {
                levelController.RefreshScene();
            }
            if (GUILayout.Button("Save"))
            {
                levelController.SaveLevel();
            }
            if (GUILayout.Button("Load"))
            {
                levelController.LoadLevel();
            }
        }
    }
}
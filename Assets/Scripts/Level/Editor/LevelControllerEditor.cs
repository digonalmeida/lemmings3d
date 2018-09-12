using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LevelController))]
public class LevelControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var levelController = (LevelController)target;
        if (GUILayout.Button("SetBlock"))
        {
            levelController.SetInputBlock();
        }
        if (GUILayout.Button("Refresh"))
        {
            levelController.Refresh();
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

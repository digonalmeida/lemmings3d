using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorController : MonoBehaviour
{

    [SerializeField]
    private LevelEditorCursor cursor;

    private LevelController levelController;

    private void Awake()
    {
        levelController = FindObjectOfType<LevelController>();
        cursor.OnActivate += OnCursorActivated;
    }

    private void OnDestroy()
    {
        cursor.OnActivate -= OnCursorActivated;
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            levelController.ChangeType();
        }
    }
    void OnCursorActivated()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            levelController.SetBlockEmpty(cursor.selectedPosition);
        }
        else if(Input.GetKey(KeyCode.LeftControl))
        {
            levelController.Rotate(cursor.selectedPosition);
        }
        else
        {
            levelController.AddBlock(cursor.addPosition);
        }
        cursor.UpdateCursorSelection();
    }


}

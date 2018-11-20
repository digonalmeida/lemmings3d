namespace LevelMap
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MapEditorController : MonoBehaviour
    {
        public static Action OnUpdate;

        private static Action OnToggleMapEditor;

        [SerializeField]
        private MapEditorCursor cursor = null;

        [SerializeField]
        private MapBlock blockBrush = null;

        private MapController mapController;
        
        private List<string> numkeyNames = new List<string>();

        public MapBlock.BlockType BrushType
        {
            get
            {
                if (blockBrush == null)
                {
                    return MapBlock.BlockType.Empty;
                }
                return blockBrush.Type;
            }
            set
            {
                if (blockBrush != null)
                {
                    blockBrush.Type = value;
                }
                NotifyUpdate();
            }
        }

        private void NotifyUpdate()
        {
            if (OnUpdate != null)
            {
                OnUpdate();
            }
        }
        public static void ToggleMapEditor()
        {
            if (OnToggleMapEditor != null)
            {
                OnToggleMapEditor();
            }
        }

        private void Toggle()
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }

        private void Awake()
        {
            mapController = FindObjectOfType<MapController>();
            cursor.OnActivate += OnCursorActivated;

            for (int i = 0; i < 10; i++)
            {
                numkeyNames.Add(i.ToString());
            }

            SetBrushId(1);

            OnToggleMapEditor += Toggle;
        }

        private void OnDestroy()
        {
            cursor.OnActivate -= OnCursorActivated;
            OnToggleMapEditor -= Toggle;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ChangeBrushType();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateBrush();
            }

            for (int i = 0; i < numkeyNames.Count; i++)
            {
                if (Input.GetKeyDown(numkeyNames[i]))
                {
                    SetBrushId(i);
                }
            }
        }

        public void SetBrushType(MapBlock.BlockType type)
        {

        }

   

        private void ChangeBrushType()
        {
            SetBrushId((int)blockBrush.Type + 1);
        }

        private void RotateBrush()
        {
            blockBrush.Rotate();
            cursor.SetCursorBlock(blockBrush);
        }

        private void SetBrushId(int id)
        {
            blockBrush.Type = (MapBlock.BlockType)(id % 5);
            cursor.SetCursorBlock(blockBrush);
        }

        private void OnCursorActivated()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                mapController.EraseBlock(cursor.SelectedPosition);
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                mapController.Rotate(cursor.SelectedPosition);
            }
            else
            {
                mapController.AddBlock(cursor.AddPosition, new MapBlock(blockBrush));
            }

            cursor.UpdateCursorSelection();
        }
    }
}
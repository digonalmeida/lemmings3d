namespace LevelMap
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MapEditorController : MonoBehaviour
    {
        
        public static Action OnUpdate;

        [SerializeField]
        private MapEditorCursor cursor = null;

        [SerializeField]
        private MapBlock blockBrush = null;

        private MapController mapController;
        
        private List<string> numkeyNames = new List<string>();
        
        [SerializeField]
        private GameObject contentGameObject = null;

        [SerializeField]
        private bool _activeOnAwake;

        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                if (contentGameObject != null)
                {
                    contentGameObject.SetActive(_isActive);
                }
            }
        }
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

        private void OnValidate()
        {
            contentGameObject = transform.GetChild(0).gameObject;
            IsActive = _activeOnAwake;
        }

        private void NotifyUpdate()
        {
            if (OnUpdate != null)
            {
                OnUpdate();
            }
        }
        
        private void Toggle()
        {
            IsActive = !IsActive;
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

            GameEvents.UI.ToggleMapEditor += Toggle;
        }

        private void OnDestroy()
        {
            cursor.OnActivate -= OnCursorActivated;
            GameEvents.UI.ToggleMapEditor -= Toggle;
        }

        private void Update()
        {
            if (!IsActive)
            {
                return;
            }
            
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
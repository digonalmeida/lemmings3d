﻿namespace LevelMap
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Renderer))]
    public class MapEditorCursor : MonoBehaviour
    {
        [SerializeField]
        private float interval = 0.1f;

        private MapBlockController blockController;
        private bool isVisible = false;
        private float timeout = 0.0f;
        private State state;
        private RaycastHit[] hitInfos = new RaycastHit[1];
        private int blockLayerMask = 0;

        public enum State
        {
            Inactive,
            Active
        }
        
        public Vector3Int SelectedPosition { get; private set; }

        public Vector3Int AddPosition { get; private set; }

        public Action OnActivate { get; set; }

        public bool IsVisible
        {
            get
            {
                return isVisible;
            }

            private set
            {
                isVisible = value;
                if (blockController != null)
                {
                    blockController.gameObject.SetActive(isVisible);
                }
            }
        }
        
        public void SetCursorBlock(MapBlock block)
        {
            if (blockController != null)
            {
                Destroy(blockController.gameObject);
            }

            blockController = MapManager.Instance.InstantiateSceneBlock(block);
            if (blockController != null)
            {
                blockController.transform.position = transform.position;
                blockController.transform.parent = transform;
                SetLayerRecursive(blockController.transform, 2); // ignore raycast
            }
        }

        public void SetLayerRecursive(Transform t, int layer)
        {
            t.gameObject.layer = layer;
            foreach (Transform child in t)
            {
                SetLayerRecursive(child, layer);
            }
        }

        public void UpdateCursorSelection()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int hits = Physics.RaycastNonAlloc(ray, hitInfos, 1000, blockLayerMask);
            IsVisible = hits > 0;
            for (int i = 0; i < hits; i++)
            {
                var hitInfo = hitInfos[i];
                var block = hitInfo.collider.GetComponent<MapBlockController>();
                if (block != null)
                {
                    SelectedPosition = Vector3Int.RoundToInt(hitInfo.collider.transform.position);
                    var position = SelectedPosition + (hitInfo.normal.normalized * 1.0f);
                    AddPosition = Vector3Int.RoundToInt(position);
                }
                else
                {
                    SelectedPosition = Vector3Int.RoundToInt(hitInfo.point);
                    var position = SelectedPosition + (hitInfo.normal.normalized * 0.5f);
                    AddPosition = Vector3Int.RoundToInt(position);
                }

                transform.position = AddPosition;
            }
        }

        private void Awake()
        {
            blockLayerMask = LayerMask.GetMask("Wall");
        }

        private void SetState(State state)
        {
            this.state = state;
            switch (state)
            {
                case State.Inactive:
                    break;
                case State.Active:
                    timeout = 0;
                    break;
                default:
                    break;
            }
        }

        private void UpdateActive()
        {
            timeout -= Time.deltaTime;
            if (timeout <= 0)
            {
                timeout = interval;
                if (OnActivate != null)
                {
                    OnActivate();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                SetState(State.Inactive);
            }
        }
        
        private void UpdateInactive()
        {
            UpdateCursorSelection();

            if (isVisible)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    SetState(State.Active);
                }
            }
        }

        private void Update()
        {
            switch (state)
            {
                case State.Inactive:
                    UpdateInactive();
                    break;
                case State.Active:
                    UpdateActive();
                    break;
                default:
                    break;
            }
        }
    }
}
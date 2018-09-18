using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class LevelEditorCursor : MonoBehaviour
{
    [SerializeField]
    private GameObject arrow = null;

    [SerializeField]
    public float interval = 0.1f;

    private Renderer renderer = null;
    private bool isVisible = false;

    public Vector3Int selectedPosition { get; private set; }
    public Vector3Int addPosition { get; private set; }
    public Action OnActivate;
    private float timeout = 0.0f;

    public bool IsVisible
    {
        get
        {
            return isVisible;
        }
        private set
        {
            isVisible = value;
            if(renderer != null)
            {
                renderer.enabled = isVisible;
            }
        }
    }

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    public enum State
    {
        Inactive,
        Active
    }

    private State state;

    public void SetState(State state)
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
        if(timeout <= 0)
        {
            timeout = interval;
            if(OnActivate != null)
            {
                OnActivate();
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            SetState(State.Inactive);
        }
    }

    public void UpdateCursorSelection()
    {
        RaycastHit hitInfo;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hitted = Physics.Raycast(ray, out hitInfo, 1000, LayerMask.GetMask("Wall"));
        IsVisible = hitted;
        if (isVisible)
        {
            var block = hitInfo.collider.GetComponent<LevelBlockController>();
            if (block != null)
            {
                selectedPosition = Vector3Int.RoundToInt(hitInfo.collider.transform.position);
                var position = selectedPosition + (hitInfo.normal.normalized * 1.0f);
                addPosition = Vector3Int.RoundToInt(position);
            }
            else
            {
                selectedPosition = Vector3Int.RoundToInt(hitInfo.point);
                var position = selectedPosition + (hitInfo.normal.normalized * 0.5f);
                addPosition = Vector3Int.RoundToInt(position);
            }

            transform.position = addPosition;

        }
    }

    private void UpdateInactive()
    {
        UpdateCursorSelection();

        if(isVisible)
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

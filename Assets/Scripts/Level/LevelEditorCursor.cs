using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class LevelEditorCursor : MonoBehaviour
{
    [SerializeField]
    private GameObject arrow = null;

    private Renderer renderer = null;
    private bool isVisible = false;

    public Vector3Int selectedPosition { get; private set; }
    public Vector3Int addPosition { get; private set; }

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

    private void Update()
    {
        RaycastHit hitInfo;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hitted = Physics.Raycast(ray, out hitInfo, 1000, LayerMask.GetMask("Wall"));
        IsVisible = hitted;
        if(isVisible)
        {
            selectedPosition = Vector3Int.RoundToInt(hitInfo.point);
            var position = selectedPosition + (hitInfo.normal.normalized * 0.5f);
            addPosition = Vector3Int.RoundToInt(position);
            transform.position = addPosition;
        }
    }
}

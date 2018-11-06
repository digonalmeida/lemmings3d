using LevelMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MapEditorGUI : MonoBehaviour
{
    [SerializeField]
    private Dropdown blockBrushDropdown;

    [SerializeField]
    private MapEditorController mapEditorController;

    List<MapBlock.BlockType> blockTypes;

    public void SaveMap()
    {
        ControllerManager.Instance.mapController.SaveLevel();
    }

    public void LoadMap()
    {
        ControllerManager.Instance.mapController.LoadLevel();
    }

    public void Refresh()
    {
        ControllerManager.Instance.mapController.RefreshScene();
    }

    public void Start()
    {
        UpdateUi();
    }

    private void Awake()
    {
        mapEditorController = FindObjectOfType<MapEditorController>();
        LoadBrushTypes();
        MapEditorController.OnUpdate += UpdateUi;
    }

    private void OnDestroy()
    {
        MapEditorController.OnUpdate -= UpdateUi;
    }

    private void LoadBrushTypes()
    {
        blockTypes = Enum.GetValues(typeof(MapBlock.BlockType)).Cast<MapBlock.BlockType>().ToList();
        blockBrushDropdown.options = blockTypes.ConvertAll((blockType) =>
        {
            var name = Enum.GetName(typeof(MapBlock.BlockType), blockType);
            return new Dropdown.OptionData(name);
        });
    }

    public void UpdateUi()
    {
        blockBrushDropdown.value = (int)mapEditorController.BrushType;
    }

    public void UpdateBlockType(int typeId)
    {
        if(!Enum.IsDefined(typeof(MapBlock.BlockType), typeId))
        {
            return;
        }
        var blockType = (MapBlock.BlockType)typeId;
        mapEditorController.BrushType = blockType;
    }
}

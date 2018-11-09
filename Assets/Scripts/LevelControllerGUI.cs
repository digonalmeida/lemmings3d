using LevelMap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelControllerGUI: MonoBehaviour
{
    [SerializeField]
    private Dropdown sceneList;

    public void StartGame()
    {
        ControllerManager.Instance.levelController.StartGame();
    }
    
    public void LoadGame()
    {
        ControllerManager.Instance.levelController.LoadGame();
    }

    public void EndGame()
    {
        ControllerManager.Instance.levelController.EndGame();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToggleEditor()
    {
        MapEditorController.ToggleMapEditor();
        //ControllerManager.Instance.mapController.ToggleMapEditor();
    }

    public void Start()
    {
        sceneList.ClearOptions();

        var mapAssets = MapManager.Instance.MapAssets;

        sceneList.options = mapAssets.ConvertAll((mapAsset) => { return new Dropdown.OptionData(mapAsset.name); });
        UpdateUi();
    }

    public void UpdateUi()
    {
        sceneList.value = MapManager.Instance.SelectedMapIndex;
    }

    public void MapSelected(int index)
    {
        MapManager.Instance.TrySelectMapById(index);
        UpdateUi();
    }
}

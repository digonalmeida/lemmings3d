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
        LevelController.Instance.StartGame();
    }
    
    public void LoadGame()
    {
        LevelController.Instance.LoadGame();
    }

    public void EndGame()
    {
        LevelController.Instance.EndGame();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToggleEditor()
    {
        GameEvents.UI.ToggleMapEditor.SafeInvoke();
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

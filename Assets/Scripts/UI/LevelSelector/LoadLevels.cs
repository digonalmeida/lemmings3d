using LevelMap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevels : MonoBehaviour
{
    //References
    public GameObject levelSelectButtonPrefab;

	// Use this for initialization
	void Start ()
    {
        List<MapAsset> mapAssets = MapManager.Instance.MapAssets;
        for(int i = 0; i < mapAssets.Count; i++)
        {
            GameObject obj = Instantiate(levelSelectButtonPrefab, this.transform);
            obj.GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
            LevelSelectButton button = obj.GetComponent<LevelSelectButton>();
            button.updateImage(mapAssets[i].mapScrenshot);
        }

        //Update Size
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 170 *  Mathf.CeilToInt(mapAssets.Count / 4f));
    }
}

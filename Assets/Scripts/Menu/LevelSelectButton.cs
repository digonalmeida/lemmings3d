using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    //Variables
    private Image imageReference;

    //Start
    private void Start()
    {
        imageReference = GetComponentInChildren<Image>();
    }

    //Update Image with level screenshot
    public void updateImage(Sprite image)
    {
        if(image != null) imageReference.sprite = image;
    }
}

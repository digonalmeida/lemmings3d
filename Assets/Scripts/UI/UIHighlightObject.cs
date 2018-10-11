using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHighlightObject : MonoBehaviour {

    public Image highlightImage;
    public HighlightPointer highlightPointerScript;

	void Update () {

        CheckForHighlightedObject();

	}

    private void CheckForHighlightedObject()
    {
        if (highlightPointerScript.isHighlighting)
        {
            highlightImage.enabled = true;
            Vector2 point;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(highlightImage.transform.parent.GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(highlightPointerScript.highlightedObject.center.position), null, out point))
            {
                highlightImage.rectTransform.anchoredPosition = point;
            }
        }
        else
        {
            highlightImage.enabled = false;
        }
    }
}

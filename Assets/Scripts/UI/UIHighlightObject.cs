using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHighlightObject : MonoBehaviour
{
    public Image highlightImage;

	void Update ()
    {
        CheckForHighlightedObject();
	}

    private void CheckForHighlightedObject()
    {
        if (HighlightPointer.Instance.isHighlighting)
        {
            highlightImage.enabled = true;
            Vector2 point;

            if (HighlightPointer.Instance.highlightedObject != null && RectTransformUtility.ScreenPointToLocalPointInRectangle(highlightImage.transform.parent.GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(HighlightPointer.Instance.highlightedObject.center.position), null, out point))
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToggleController : MonoBehaviour
{
    //Variables
    private Image togglePanelRef;

    //Start
    private void Start()
    {
        togglePanelRef = this.transform.parent.GetComponent<Image>();
    }

    //Change Color of Panel
    public void ChangeColor(bool active)
    {
        if (active) togglePanelRef.color = Color.red;
        else togglePanelRef.color = Color.white;
    }
}

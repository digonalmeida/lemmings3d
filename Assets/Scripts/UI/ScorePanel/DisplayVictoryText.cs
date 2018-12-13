using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayVictoryText : MonoBehaviour
{
    public void displayText(bool victory)
    {
        Text textRef = this.GetComponent<Text>();
        textRef.enabled = true;

        if (victory)
        {
            textRef.text = "Victory!";
            textRef.color = Color.green;
        }
        else
        {
            textRef.text = "Defeat!";
            textRef.color = Color.red;
        }
    }
	
}

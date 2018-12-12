using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMedal : MonoBehaviour
{
    public void displayMedal()
    {
        for(int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}

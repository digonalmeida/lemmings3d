using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIDock : MonoBehaviour
{

    public UnityEvent pause;
    public UnityEvent explodeAll;

    public void PauseButton()
    {
        pause.Invoke();
    }

    public void ExplodeAllButton()
    {
        explodeAll.Invoke();
    }


    public void DebugText(string text)
    {
        Debug.Log(text);
    }

}

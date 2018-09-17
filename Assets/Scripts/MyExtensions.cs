using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyExtensions  {

    public static void WaitAndAct(this MonoBehaviour mono, float waitTime, Action endAction)
    {
        mono.StartCoroutine(WaitAndAct(waitTime, endAction));
    }

    private static IEnumerator WaitAndAct(float time, Action action)
    {
        yield return new WaitForSeconds(time);

        action.Invoke();
    }
}

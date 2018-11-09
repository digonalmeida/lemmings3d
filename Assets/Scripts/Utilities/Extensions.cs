using System;
using System.Collections;
using UnityEngine;

public static class Extensions {

    public static void SafeInvoke(this Action action)
    {
        if (action != null)
        {
            action.Invoke();
        }
    }

    public static void SafeInvoke<T>(this Action<T> action, T obj)
    {
        if(action != null)
        {
            action.Invoke(obj);
        }
    }

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

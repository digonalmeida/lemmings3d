using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public delegate void LemmingDidAction();
    public static event LemmingDidAction LemmingReachedExit;
    public static event LemmingDidAction LemmingSpawned;

    public int lemmingsSpawned { get; private set; }
    public int lemmingsEnteredExit { get; private set; }

    private void OnEnable()
    {
        LemmingReachedExit += LemmingExit;
        LemmingSpawned += LemmingEnter;
    }

    private void OnDisable()
    {
        LemmingReachedExit -= LemmingExit;
        LemmingSpawned -= LemmingEnter;
    }

    private void Awake()
    {
        lemmingsSpawned = 0;
        lemmingsEnteredExit = 0;
    }

    public void LemmingExit()
    {
        lemmingsEnteredExit++;
    }

    public void LemmingEnter()
    {
        lemmingsSpawned++;
    }

    public static void TriggerLemmingReachedExit()
    {
        if(LemmingReachedExit!=null)
            LemmingReachedExit.Invoke();
    }

    public static void TriggerLemmingSpawned()
    {
        if (LemmingSpawned != null)
            LemmingSpawned.Invoke();
    }

}

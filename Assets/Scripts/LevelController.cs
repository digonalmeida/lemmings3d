using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public delegate void LemmingDidAction();
    public delegate void SpawnRateAction(int newRate);
    public static event LemmingDidAction LemmingReachedExit;
    public static event LemmingDidAction LemmingSpawned;
    public static event LemmingDidAction LemmingUsedSkill;
    public static event SpawnRateAction ChangedSpawnRate;

    public static int lemmingsSpawned { get; private set; }
    public static int lemmingsEnteredExit { get; private set; }

    public static int minimumSpawnRate = 30;
    public static int maximumSpawnRate = 70;
    public static int currentRate = 50;

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

    public static void TriggerLemmingUsedSkill()
    {
        if (LemmingUsedSkill != null)
            LemmingUsedSkill.Invoke();
    }

    public static void ChangeSpawnRate(int increment)
    {
        if ((currentRate + increment) >= minimumSpawnRate && (currentRate + increment) <= maximumSpawnRate)
        {
            currentRate += increment;
            TriggerSpawnRateChange(currentRate);
        }
    }

    public static void TriggerSpawnRateChange(int newRate)
    {
        if (ChangedSpawnRate != null)
            ChangedSpawnRate.Invoke(newRate);
    }

}

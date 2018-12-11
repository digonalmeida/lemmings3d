using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgressBar : MonoBehaviour
{
    public Player team;
    public Transform holder;
    public PlayerProgressUnit fillUnitPrefab;
    public PlayerProgressTarget fillTargetPrefab;
    public GameObject animatedLemming;
    private List<PlayerProgressUnit> units = new List<PlayerProgressUnit>();
    private PlayerProgressTarget target;
    private int minimum, maximum;

    public int fill;
    private int lastFill;

    void OnEnable()
    {
        GameEvents.NetworkLemmings.LemmingReachedExit += UpdateBar;
    }

    void OnDisable()
    {
        GameEvents.NetworkLemmings.LemmingReachedExit -= UpdateBar;
    }

    void Update()
    {
        if (fill != lastFill)
        {
            UpdateFill(fill);
        }
        lastFill = fill;
    }

    public void Initialize(int totalLemmings, int minimumLemmings)
    {
        maximum = totalLemmings;
        minimum = minimumLemmings;
        for (int i = 0; i < totalLemmings; i++)
        {
            PlayerProgressUnit unit = Instantiate(fillUnitPrefab, holder);
            units.Add(unit);

            if (i == (totalLemmings - minimum - 1))
            {
                target = Instantiate(fillTargetPrefab, holder);
            }
        }

        Reset();
    }

    void UpdateFill(int newFill)
    {
        if (newFill > 0)
        {
            units[units.Count - newFill].TurnOn(newFill / maximum);
        }

        if (newFill == minimum)
        {
            target.Trigger();
        }
    }

    private void UpdateBar(LemmingStateController lemming)
    {
        var dict = LevelController.Instance.gameStateManager.lemmingsEnteredExit;
        int count = dict[team];

        for (int i = 0; i < count; i++)
        {
            if(!units[units.Count - i].on){
                UpdateFill(count);
            }
        }

    }


    public void Clear()
    {
        foreach (Transform child in holder)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    void Reset()
    {
        foreach (PlayerProgressUnit unit in units)
        {
            unit.Reset();
        }
    }


}

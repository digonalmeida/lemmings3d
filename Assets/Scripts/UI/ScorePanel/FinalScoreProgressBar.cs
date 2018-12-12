using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScoreProgressBar : MonoBehaviour
{
    //References
    public Transform holder;
    public PlayerProgressUnit fillUnitPrefab;
    public PlayerProgressTarget fillTargetPrefab;
    public DisplayMedal displayMedal;
    public PlayerProgressBar localProgressBar;
    public PlayerProgressBar opponentProgressBar;

    //Control Variables
    private PlayerProgressTarget target;
    private List<PlayerProgressUnit> units = new List<PlayerProgressUnit>();
    private int minimum, maximum;
    private int targetFill;
    private int currentFill = 0;
    private float currentTimer;
    public float updateTimer = 0.25f;
    
    //Start
    private void Start()
    {
        currentTimer = updateTimer;
        maximum = LevelController.Instance.CurrentMapSettings.LemmingsCount;
        minimum = LevelController.Instance.CurrentMapSettings.MinimumVictoryCount;
        targetFill = localProgressBar.fill;

        for (int i = 0; i < maximum; i++)
        {
            PlayerProgressUnit unit = Instantiate(fillUnitPrefab, holder);
            units.Add(unit);

            if (i == (maximum - minimum - 1))
            {
                target = Instantiate(fillTargetPrefab, holder);
            }
        }

        Reset();
    }

    void Reset()
    {
        foreach (PlayerProgressUnit unit in units)
        {
            unit.Reset();
        }
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

    void Update()
    {
        currentTimer -= Time.deltaTime;
        if(currentTimer <= 0f)
        {
            currentTimer = updateTimer;
            if (currentFill < targetFill)
            {
                currentFill++;
                UpdateFill(currentFill);
            }
            else if (currentFill == targetFill)
            {
                currentFill++;
                if(localProgressBar.fill >= opponentProgressBar.fill)
                {
                    displayMedal.displayMedal();
                }
            }
        }
    }
}

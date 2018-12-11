using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgressBar : MonoBehaviour
{
    public Transform holder;
    public PlayerProgressUnit fillUnitPrefab;
    private List<PlayerProgressUnit> units = new List<PlayerProgressUnit>();

    public int fill;
    private int lastFill;

    void Start()
    {
        Initialize(10);
    }

    void Update()
    {
        if (fill != lastFill)
        {
            UpdateFill(fill);
        }
        lastFill = fill;
    }

    void Initialize(int totalLemmings)
    {
        for (int i = 0; i < totalLemmings; i++)
        {
            PlayerProgressUnit unit = Instantiate(fillUnitPrefab, holder);
            unit.Reset();
            units.Add(unit);
        }
    }

    void UpdateFill(int newFill)
    {
        if (newFill > 0)
        {
            units[units.Count - newFill].TurnOn(newFill/10);
        }
    }


}

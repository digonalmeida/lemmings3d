using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomProp : MonoBehaviour {
    public List<GameObject> prefabs;
    public int currentPropIndex = -1;
    private int lastPropIndex = -1;
    public GameObject currentProp = null;

    void UpdateProp()
    {
        if(currentProp != null)
        {
            Destroy(currentProp);
        }
        if(currentPropIndex >= 0 && currentPropIndex < prefabs.Count)
        {
            currentProp = Instantiate(prefabs[currentPropIndex], transform.position, Quaternion.identity, transform);
        }
        lastPropIndex = currentPropIndex;
    }

    void RandomizeProp()
    {
        currentPropIndex = Random.Range(-120, prefabs.Count);
        UpdateProp();
    }

    void Awake()
    {
        RandomizeProp();
    }

    void Update()
    {
        if(currentPropIndex != lastPropIndex)
        {
            UpdateProp();
        }
    }
}

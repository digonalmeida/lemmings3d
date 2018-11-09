using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPropSpawner : MonoBehaviour
{
    public List<GameObject> prefabs;
    public int currentPropIndex = -1;
    public GameObject currentProp = null;

    void SpawnProp()
    {
        if(currentProp != null)
        {
            Destroy(currentProp);
        }
        if(currentPropIndex >= 0 && currentPropIndex < prefabs.Count)
        {
            currentProp = Instantiate(prefabs[currentPropIndex], transform.position, Quaternion.identity, transform);
        }
    }

    public void TrySpawnProp(float chance)
    {
        if (Random.Range(0.0f, 1.0f) < chance)
        {
            RandomizeProp();
        }
        else
        {
            currentPropIndex = -1;
        }
        SpawnProp();
    }

    public void RandomizeProp()
    {
        currentPropIndex = Random.Range(0, prefabs.Count);
    }

    public void Init(bool enabled)
    {
        if (enabled)
        {
            RandomizeProp();
            SpawnProp();
        }
        else
        {
            if(Application.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}
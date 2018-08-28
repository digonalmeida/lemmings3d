using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField]
    private GameObject spawnable = null;
    [SerializeField]
    private int count = 10;
    [SerializeField]
    private float interval = 1;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        for(var i = 0; i < count; i++)
        {
            Spawn();
            yield return new WaitForSeconds(interval);
        }
    }

    private void Spawn()
    {
        Instantiate(spawnable, transform.position, transform.rotation);
    }
}

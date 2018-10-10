using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Physics : MonoBehaviour
{
    //Control Variables
    [SerializeField]
    private GameObject spawnable = null;
    public int count = 10;
    public float interval = 1f;
    public Direction startingMovementDirection;

    //Internal Variables
    private Stack<GameObject> lemmingsPool;
    private float currentTimer;

    //Start Method
    private void Start()
    {
        currentTimer = 0f;
        lemmingsPool = new Stack<GameObject>();
        for (int i = 0; i < count; i++)
        {
            GameObject obj = createLemming();
            obj.SetActive(false);
            lemmingsPool.Push(obj);
        }
    }

    //Create Lemming
    private GameObject createLemming()
    {
        GameObject obj = Instantiate(spawnable, this.transform.position, this.transform.rotation);

        return obj;
    }

    //Update Method
    private void Update()
    {
        if(count > 0)
        {
            if (currentTimer <= 0f)
            {
                currentTimer = interval;
                count--;
                spawnLemming();
            }
            else currentTimer -= Time.deltaTime;
        }
    }

    //Spawn New Lemming
    private void spawnLemming()
    {
        if (lemmingsPool.Count > 0)
        {
            GameObject obj = lemmingsPool.Pop();
            obj.SetActive(true);
        }
        else
        {
            GameObject obj = createLemming();
            lemmingsPool.Push(obj);
        }

        LevelController.TriggerLemmingSpawned();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
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

    public void Init()
    {
        var block = GetComponent<LevelMap.MapBlockController>();
        if(block != null)
        {
            startingMovementDirection = block.Block.Direction;
        }

        currentTimer = 0f;
        lemmingsPool = new Stack<GameObject>();
        for (int i = 0; i < count; i++)
        {
            GameObject obj = createLemming();
            obj.SetActive(false);
            lemmingsPool.Push(obj);
        }

        ChangeInterval(LevelController.currentRate);
        StartCoroutine(SpawnRoutine());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        Stop();
    }

    private void OnEnable()
    {
        GameEvents.Lemmings.ChangedSpawnRate += ChangeInterval;
        GameEvents.GameState.OnStartGame += Init;
        GameEvents.GameState.OnEndGame += Stop;
    }

    private void OnDisable()
    {
        GameEvents.Lemmings.ChangedSpawnRate -= ChangeInterval;
        GameEvents.GameState.OnStartGame -= Init;
        GameEvents.GameState.OnEndGame -= Stop;
    }

    //Create Lemming
    private GameObject createLemming()
    {
        GameObject obj = Instantiate(spawnable, this.transform.position, this.transform.rotation);
        var movController = obj.GetComponent<LemmingMovementController>();
        if(movController != null)
        {
            movController.SetDirection(startingMovementDirection);
            movController.SetForwardDirection(startingMovementDirection);
        }
        return obj;
    }
    
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (count > 0)
            {
                if (currentTimer <= 0f)
                {
                    currentTimer = interval;
                    count--;
                    spawnLemming();
                }
                else
                {
                    currentTimer -= Time.deltaTime;
                }
            }
            yield return null;
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

        GameEvents.Lemmings.LemmingSpawned.SafeInvoke();
    }

    private void ChangeInterval(int newSpawnRate)
    {
        interval = 1f + ((float)(60 - newSpawnRate)) / 20f;
    }
}

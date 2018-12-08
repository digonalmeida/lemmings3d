using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Control Variables
    [SerializeField]
    private GameObject spawnable = null;
    public int count = 1;
    public float interval = 1f;
    public Direction startingMovementDirection;
    public Player team;

    //Internal Variables
    private Stack<GameObject> lemmingsPool;
    private float currentTimer;

    private void Start(){
        Setup();
    }

    public void Setup()
    {
        var block = GetComponent<LevelMap.MapBlockController>();
        if (block != null)
        {
            startingMovementDirection = block.Block.Direction;
            team = block.Block.Team;
        }

        currentTimer = 0f;
        lemmingsPool = new Stack<GameObject>();
        for (int i = 0; i < count; i++)
        {
            GameObject obj = createLemming();
            obj.SetActive(false);
            lemmingsPool.Push(obj);
        }
    }

    public void Init()
    {
        ChangeInterval();
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
        LemmingMovementController movController = obj.GetComponent<LemmingMovementController>();
        if (movController != null)
        {
            movController.SetDirection(startingMovementDirection);
            movController.SetForwardDirection(startingMovementDirection);
        }
        LemmingStateController stateController = obj.GetComponent<LemmingStateController>();
        if (stateController != null)
        {
            stateController.setTeam(team);
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
        GameObject obj;
        if (lemmingsPool.Count > 0)
        {
            obj = lemmingsPool.Pop();
            obj.SetActive(true);
        }
        else
        {
            obj = createLemming();
            lemmingsPool.Push(obj);
        }
        LemmingStateController lemmingAIScript = obj.GetComponent<LemmingStateController>();
        GameEvents.Lemmings.LemmingSpawned.SafeInvoke(lemmingAIScript);
    }

    private void ChangeInterval()
    {
        interval = 1f + ((float)(60 - LevelController.Instance.currentSpawnRate)) / 20f;
    }

}

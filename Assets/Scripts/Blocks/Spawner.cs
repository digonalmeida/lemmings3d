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

    private LemmingSpawnInfo lemmingSpawnInfo = new LemmingSpawnInfo();

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
            UpdateTeamColor();
        }
    }

    public void UpdateTeamColor()
    {
        var renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            return;
        }
        
        switch (team)
        {
            case Player.None:
                renderer.material.color = Color.gray;
                break;
            case Player.Player1:
                renderer.material.color = Color.yellow;
                break;
            case Player.Player2:
                renderer.material.color = Color.magenta;
                break;
        }
    }

    private void LateUpdate()
    {
        UpdateTeamColor();
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
        lemmingSpawnInfo.position = transform.position;
        lemmingSpawnInfo.startingMovementDirection = startingMovementDirection;
        lemmingSpawnInfo.team = team;
        GameEvents.Lemmings.OnSpawnRequest.SafeInvoke(lemmingSpawnInfo);
    }

    private void ChangeInterval()
    {
        interval = 1f + ((float)(60 - LevelController.Instance.currentSpawnRate)) / 20f;
    }

}

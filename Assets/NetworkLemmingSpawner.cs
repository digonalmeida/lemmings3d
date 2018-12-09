using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class LemmingSpawnInfo
{
    public Vector3 position;
    public Direction startingMovementDirection;
    public Player team;
}
public class NetworkLemmingSpawner : NetworkBehaviour
{
    [SerializeField]
    private GameObject spawnable = null;
    private Player team = Player.None;

    public void Awake()
    {
        GameEvents.Lemmings.OnSpawnRequest += OnSpawnRequest;
    }

    public void OnDestroy()
    {
        GameEvents.Lemmings.OnSpawnRequest -= OnSpawnRequest;
    }

    public void OnSpawnRequest(LemmingSpawnInfo info)
    {
        if(!isServer)
        {
            return;
        }
        var lemming = createLemming(info);
        NetworkServer.Spawn(lemming);
        var stateController = lemming.GetComponent<LemmingStateController>();
        GameEvents.Lemmings.LemmingSpawned.SafeInvoke(stateController);
    }

    //Create Lemming
    private GameObject createLemming(LemmingSpawnInfo info)
    {
        GameObject obj = Instantiate(spawnable, info.position, Quaternion.identity);

        LemmingMovementController movController = obj.GetComponent<LemmingMovementController>();
        if (movController != null)
        {
            movController.SetDirection(info.startingMovementDirection);
            movController.SetForwardDirection(info.startingMovementDirection);
        }
        LemmingStateController stateController = obj.GetComponent<LemmingStateController>();
        if (stateController != null)
        {
            stateController.setTeam(info.team);
        }
        return obj;
    }
}

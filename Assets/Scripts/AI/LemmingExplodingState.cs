using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingExplodingState : LemmingState
{
    public LemmingExplodingState()
        : base()
    {
        AnimationName = "exploding";
    }

    public override void Enter()
    {
        base.Enter();
        Agent.MovementController.SetDirection(Direction.None);
    }

    public override void OnGetNextWaypoint()
    {
        base.OnGetNextWaypoint();

        //Review Positions
        ControllerManager.Instance.mapController.EraseBlock(Vector3Int.FloorToInt(Agent.transform.position + Vector3.down));
        ControllerManager.Instance.mapController.EraseBlock(Vector3Int.FloorToInt(Agent.transform.position + Vector3.up));
        ControllerManager.Instance.mapController.EraseBlock(Vector3Int.FloorToInt(Agent.transform.position + Vector3.left));
        ControllerManager.Instance.mapController.EraseBlock(Vector3Int.FloorToInt(Agent.transform.position + Vector3.right));
        ControllerManager.Instance.mapController.EraseBlock(Vector3Int.FloorToInt(Agent.transform.position + Vector3.forward));
        ControllerManager.Instance.mapController.EraseBlock(Vector3Int.FloorToInt(Agent.transform.position + Vector3.back));
    }

    public override void Update()
    {
        base.Update();
        if (Agent.AnimationController.isEndOfAnimation("exploding")) Agent.StateController.killLemming();
    }
}

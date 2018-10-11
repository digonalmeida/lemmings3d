using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingDiggingState : LemmingState
{
    public LemmingDiggingState()
        : base()
    {
        AnimationName = "blocking";
        //TODO
    }

    public override void Enter()
    {
        base.Enter();
        Agent.MovementController.SetDirection(Direction.None);
    }

    public override void OnGetNextWaypoint()
    {
        base.OnGetNextWaypoint();

        //Must change this to complete animation first!
        ControllerManager.Instance.mapController.EraseBlock(Vector3Int.FloorToInt(Agent.transform.position + Vector3.down));

        Agent.StateController.dequeueSkill();
        this.StateMachine.TriggerEvent((int)LemmingAI.Trigger.FinishedTask);
    }

    public override void Exit()
    {
        base.Exit();
        Agent.MovementController.SetDirectionForward();
    }
}

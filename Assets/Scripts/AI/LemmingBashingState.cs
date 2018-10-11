﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingBashingState : LemmingState
{
    public LemmingBashingState()
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
        Vector3 forward = Directions.GetWorldDirection(Agent.MovementController.getForwardDirection());
        ControllerManager.Instance.mapController.EraseBlock(new Vector3Int(Mathf.CeilToInt(Agent.transform.position.x + forward.x), Mathf.CeilToInt(Agent.transform.position.y + forward.y), Mathf.CeilToInt(Agent.transform.position.z + forward.z)));

        Agent.StateController.dequeueSkill();
        this.StateMachine.TriggerEvent((int)LemmingAI.Trigger.FinishedTask);
    }

    public override void Exit()
    {
        base.Exit();
        Agent.MovementController.SetDirectionForward();
    }
}

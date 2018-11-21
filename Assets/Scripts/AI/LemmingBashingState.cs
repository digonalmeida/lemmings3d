﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingBashingState : LemmingState
{
    public override void Enter()
    {
        base.Enter();
        Agent.AnimationController.setTrigger("DigForward");
        Agent.MovementController.SetDirection(Direction.None);
    }

    public override void Exit()
    {
        base.Exit();
        Agent.MovementController.SetDirectionForward();
    }

    public override void Update()
    {
        base.Update();
        if (Agent.AnimationController.isEndOfAnimation("DigForward"))
        {
            Vector3 forward = Directions.GetWorldDirection(Agent.MovementController.getForwardDirection());
            LevelMap.MapController.Instance.EraseWall(new Vector3Int(Mathf.CeilToInt(Agent.transform.position.x + forward.x), Mathf.CeilToInt(Agent.transform.position.y + forward.y), Mathf.CeilToInt(Agent.transform.position.z + forward.z)));
            Agent.StateController.dequeueSkill();
            this.StateMachine.TriggerEvent((int)LemmingAI.Trigger.FinishedTask);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingDiggingState : LemmingState
{
    public override void Enter()
    {
        base.Enter();
        Agent.AnimationController.setTrigger("DigDown");
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
        if(Agent.AnimationController.isEndOfAnimation("DigDown"))
        {
            LevelMap.MapController.Instance.EraseWall(Vector3Int.FloorToInt(Agent.transform.position + Vector3.down));
            Agent.StateController.dequeueSkill();
            this.StateMachine.TriggerEvent((int)LemmingAI.Trigger.FinishedTask);
        }
    }
}

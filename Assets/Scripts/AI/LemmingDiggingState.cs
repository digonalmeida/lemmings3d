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
        Agent.AnimationController.finishedAnimationAction += finishedAnimation;
    }

    public override void Exit()
    {
        base.Exit();
        Agent.MovementController.SetDirectionForward();
        Agent.AnimationController.finishedAnimationAction -= finishedAnimation;
    }

    public void finishedAnimation()
    {
        LevelMap.MapController.Instance.EraseWall(Vector3Int.FloorToInt(Agent.transform.position + Vector3.down));
        Agent.StateController.dequeueSkill();
        this.StateMachine.TriggerEvent((int)LemmingAI.Trigger.FinishedTask);
    }
}

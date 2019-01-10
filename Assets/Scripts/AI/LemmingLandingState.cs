using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingLandingState : LemmingState
{
    public override void Enter()
    {
        base.Enter();
        Agent.AnimationController.setBool("Falling", false);
        Agent.MovementController.SetDirection(Direction.None);
        Agent.AnimationController.finishedAnimationAction += finishedAnimation;
    }

    public void finishedAnimation()
    {
        Agent.AnimationController.finishedAnimationAction -= finishedAnimation;
        this.StateMachine.TriggerEvent((int)LemmingAI.Trigger.FinishedTask);
        Agent.MovementController.SetDirectionForward();
    }
}
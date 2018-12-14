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
    }

    public override void Update()
    {
        base.Update();
        if(Agent.StateController.isFloater())
        {
            if (Agent.AnimationController.isEndOfAnimation("Floater_End"))
            {
                this.StateMachine.TriggerEvent((int)LemmingAI.Trigger.FinishedTask);
                Agent.MovementController.SetDirectionForward();
            }
        }
        else
        {
            if (Agent.AnimationController.isEndOfAnimation("Landing"))
            {
                this.StateMachine.TriggerEvent((int)LemmingAI.Trigger.FinishedTask);
                Agent.MovementController.SetDirectionForward();
            }
        }
    }
}
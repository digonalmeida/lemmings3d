using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingWalkingState : LemmingState
{
    public LemmingWalkingState()
        : base()
    {
        AnimationName = "walking";
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void OnGetNextWaypoint()
    {
        base.OnGetNextWaypoint();

        if(Agent.MovementController.CheckExitPoint())
        {
            StateMachine.TriggerEvent((int)LemmingAI.Trigger.ArrivedAtExit);
            return;
        }

        var changeDirectionOrder = Agent.MovementController.CheckChangeDirectionOrders();
        if (changeDirectionOrder != Direction.None)
        {
            Agent.MovementController.SetForwardDirection(changeDirectionOrder);
        }

        if (Agent.MovementController.CheckWallForward())
        {
            Agent.MovementController.TurnAround();            
            return;
        }

        Agent.MovementController.SetDirectionForward();
    }
}
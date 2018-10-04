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
        Agent.MovementController.SetWaypointForward();
    }

    public override void OnGetNextWaypoint()
    {
        base.OnGetNextWaypoint();

        /*
        if (!Agent.MovementController.CheckFloor())
        {
            StateMachine.SetState(Agent.FallingState);
            return;
        }
        */

        var changeDirectionOrder = Agent.MovementController.CheckChangeDirectionOrders();
        if(changeDirectionOrder != Direction.None)
        {
            Agent.MovementController.SetFacingDirection(changeDirectionOrder);
        }

        if (Agent.MovementController.CheckWallForward())
        {
            Agent.MovementController.SetWaypointTurnAround();            
            return;
        }

        Agent.MovementController.SetWaypointForward();
    }
}
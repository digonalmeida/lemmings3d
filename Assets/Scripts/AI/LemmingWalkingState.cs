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

        var changeDirectionOrder = Agent.MovementController.CheckChangeDirectionOrders();
        if (changeDirectionOrder != Direction.None)
        {
            Debug.Log("here");
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
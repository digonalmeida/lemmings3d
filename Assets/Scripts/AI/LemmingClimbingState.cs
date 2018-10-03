using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingClimbingState : LemmingState
{
    public LemmingClimbingState()
        : base()
    {
        AnimationName = "climbing";
    }

    public override void Enter()
    {
        base.Enter();
        Agent.MovementController.SetWaypointClimbing();
    }

    public override void OnArrivedAtWaypoint()
    {
        base.OnArrivedAtWaypoint();

        if (!Agent.MovementController.CheckWallForward())
        {
            StateMachine.SetState(Agent.WalkingState);
            return;
        }

        Agent.MovementController.SetWaypointClimbing();
    }
}
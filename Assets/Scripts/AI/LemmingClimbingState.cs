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
    }

    public override void OnGetNextWaypoint()
    {
        base.OnGetNextWaypoint();
        if(Agent.MovementController.CheckWallForward())
        {
            Agent.MovementController.SetWaypointClimbing();
        }
        else
        {
            Agent.MovementController.SetWaypointForward();
        }
        
    }
}
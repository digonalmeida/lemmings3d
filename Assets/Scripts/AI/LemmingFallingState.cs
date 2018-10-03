using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingFallingState : LemmingState
{
    public LemmingFallingState()
        : base()
    {
        AnimationName = "falling";
    }

    public override void Enter()
    {
        base.Enter();
        Agent.MovementController.SetWaypointFalling();
    }

    public override void OnArrivedAtWaypoint()
    {
        base.OnArrivedAtWaypoint();

        if(Agent.MovementController.CheckFloor())
        {
            StateMachine.SetState(Agent.WalkingState);
        }
        else
        {
            Agent.MovementController.SetWaypointFalling();
        }
        
    }
}
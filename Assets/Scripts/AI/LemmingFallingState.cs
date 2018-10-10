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
        Agent.MovementController.SetDirectionDown();
    }

    public override void OnGetNextWaypoint()
    {
        base.OnGetNextWaypoint();

        Agent.MovementController.SetDirectionDown();
    }
}
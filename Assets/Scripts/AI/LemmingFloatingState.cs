using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingFloatingState : LemmingState
{
    public LemmingFloatingState()
        : base()
    {
        AnimationName = "floating";
    }

    public override void Enter()
    {
        base.Enter();
        Agent.MovementController.SetDirection(Direction.Down);
    }

    public override void OnGetNextWaypoint()
    {
        base.OnGetNextWaypoint();
        Agent.MovementController.SetDirection(Direction.Down);
    }
}

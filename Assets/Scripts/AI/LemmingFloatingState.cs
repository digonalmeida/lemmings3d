using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingFloatingState : LemmingState
{
    public override void Enter()
    {
        base.Enter();
        Agent.AnimationController.setBool("Floater", true);
        Agent.MovementController.SetDirection(Direction.Down);
    }

    public override void OnGetNextWaypoint()
    {
        base.OnGetNextWaypoint();
        Agent.MovementController.SetDirection(Direction.Down);
    }

    public override void Exit()
    {
        Agent.AnimationController.setBool("Floater", true);
        Agent.AnimationController.setBool("Falling", false);
        base.Exit();
    }
}

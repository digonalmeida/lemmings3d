using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingFallingState : LemmingState
{
    public override void Enter()
    {
        base.Enter();
        Agent.AnimationController.setBool("Falling", true);
        Agent.MovementController.SetDirection(Direction.Down);
    }

    public override void OnGetNextWaypoint()
    {
        base.OnGetNextWaypoint();

        Agent.MovementController.SetDirection(Direction.Down);
    }

    public override void Exit()
    {
        Agent.AnimationController.setBool("Falling", false);
        base.Exit();
    }
}
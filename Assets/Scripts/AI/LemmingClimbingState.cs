using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingClimbingState : LemmingState
{
    public override void Enter()
    {
        base.Enter();
        Agent.AnimationController.setBool("Climbing", true);
    }

    public override void OnGetNextWaypoint()
    {
        base.OnGetNextWaypoint();
        if (Agent.MovementController.CheckCeilling())
        {
            Agent.MovementController.SetDirection(Direction.Down);
        }
        else if (Agent.MovementController.CheckWallForward())
        {
            Agent.MovementController.SetDirection(Direction.Up);
        }
        else
        {
            Agent.MovementController.SetDirectionForward();
            Agent.AnimationController.setBool("Climbing", false);
            Agent.AnimationController.setBool("Walking", true);
        }
    }

    public override void Exit()
    {
        Agent.AnimationController.setBool("Climbing", false);
        base.Exit();
    }
}
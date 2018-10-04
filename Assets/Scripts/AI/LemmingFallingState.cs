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

    public override void OnGetNextWaypoint()
    {
        base.OnGetNextWaypoint();
        
        if(Agent.MovementController.CheckFloor())
        {
            StateMachine.SetState(Agent.WalkingState);
            Agent.Animator.Play("walking");
        }
        else
        {
            Agent.MovementController.SetWaypointFalling();
            Agent.Animator.Play("falling");
            Debug.Log("here");
        }
    }
}
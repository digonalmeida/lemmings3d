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

    public override void OnArrivedAtWaypoint()
    {
        base.OnArrivedAtWaypoint();

        if (!Agent.MovementController.CheckFloor())
        {
            StateMachine.SetState(Agent.FallingState);
            return;
        }

        if (Agent.MovementController.CheckWallForward())
        {
            if(Agent.StateController.checkSkill(Skill.Climber))
            {
                StateMachine.SetState(Agent.ClimbingState);
            }
            else
            {
                Agent.MovementController.SetWaypointTurnAround();
            }
            
            return;
        }
        
        Agent.MovementController.SetWaypointForward();
    }
}
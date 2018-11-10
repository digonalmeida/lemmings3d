using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingExitingLevelState : LemmingState
{
    public LemmingExitingLevelState()
        : base()
    {
        AnimationName = "exiting";
    }

    public override void Enter()
    {
        Debug.Log("entered exit point");
        Debug.Break();
        base.Enter();
        Agent.MovementController.SetDirection(Direction.None);
        Agent.LemmingActions.EnterExitPoint();
    }

    public override void OnGetNextWaypoint()
    {
        base.OnGetNextWaypoint();
        Agent.MovementController.SetDirection(Direction.None);
    }

    public override void Update()
    {
        base.Update();
        if (Agent.AnimationController.isEndOfAnimation("exiting")) {
            Agent.LemmingActions.EliminateLemming();
        }
    }
}

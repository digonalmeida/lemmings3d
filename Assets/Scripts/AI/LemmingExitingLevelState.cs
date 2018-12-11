using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingExitingLevelState : LemmingState
{
    public override void Enter()
    {
        Debug.Log("Lemming entered state exxiting level");
        base.Enter();
        Agent.GetComponent<HighlightableObject>().canBeHighlighted = false;
        Agent.AnimationController.setBool("Walking", false);
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
        if (Agent.AnimationController.isEndOfAnimation("exiting"))
        {
            Agent.LemmingActions.EliminateLemming();
        }
    }
}

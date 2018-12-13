using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingExitingLevelState : LemmingState
{
    public override void Enter()
    {
        base.Enter();
        Agent.AnimationController.setBool("Walking", false);
        Agent.MovementController.SetDirection(Direction.None);
        Agent.LemmingActions.EnterExitPoint();
        Agent.MovementController.OnArrivedAndRotated += dispatchLemming;
        HighlightPointer.Instance.clearHighlight(Agent.GetComponent<HighlightableObject>());
        Agent.GetComponent<HighlightableObject>().canBeHighlighted = false;
    }

    public override void OnGetNextWaypoint()
    {
        base.OnGetNextWaypoint();
        Agent.MovementController.SetDirection(Direction.None);
    }

    private void dispatchLemming()
    {
        Agent.MovementController.OnArrivedAndRotated -= dispatchLemming;
        Agent.LemmingActions.EliminateLemming();
    }
}

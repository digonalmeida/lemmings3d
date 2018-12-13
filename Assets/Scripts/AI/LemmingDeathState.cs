using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingDeathState : LemmingState
{
    private bool performedAction;

    public override void Enter()
    {
        base.Enter();
        Agent.MovementController.SetDirection(Direction.None);
        Agent.AnimationController.setBool("Death", true);
        Agent.GetComponent<HighlightableObject>().canBeHighlighted = false;
        performedAction = false;
    }

    public override void Update()
    {
        base.Update();
        if (!performedAction && Agent.AnimationController.isEndOfAnimation("Fall_Death"))
        {
            performedAction = true;
            Agent.LemmingActions.KillLemming();
        }
    }
}

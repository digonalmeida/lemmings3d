using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingDeathState : LemmingState
{
    public override void Enter()
    {
        base.Enter();
        Agent.MovementController.SetDirection(Direction.None);
        Agent.AnimationController.setBool("Death", true);
        Agent.GetComponent<HighlightableObject>().canBeHighlighted = false;
    }

    public override void Update()
    {
        base.Update();
        if (Agent.AnimationController.isEndOfAnimation("death"))
        {
            Agent.LemmingActions.KillLemming();
        }
    }
}

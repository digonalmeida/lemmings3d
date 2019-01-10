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
        Agent.AnimationController.finishedAnimationAction += finishedAnimation;
    }

    public void finishedAnimation()
    {
        //Safeguard Animation Skip
        if (!Agent.AnimationController.checkCurrentAnimation("Floater_End"))
        {
            Agent.AnimationController.finishedAnimationAction -= finishedAnimation;
            Agent.LemmingActions.KillLemming();
        }
    }
}

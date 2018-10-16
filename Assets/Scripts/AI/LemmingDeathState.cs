using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingDeathState : LemmingState
{
    public LemmingDeathState()
        : base()
    {
        AnimationName = "death";
    }

    public override void Enter()
    {
        base.Enter();
        Agent.GetComponent<HighlightableObject>().canBeHighlighted = false;
    }

    public override void Update()
    {
        base.Update();
        if (Agent.AnimationController.isEndOfAnimation("death"))
        {
            Agent.GetComponent<LemmingStateController>().killLemming();
        }
    }
}

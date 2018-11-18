using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingBlockingState : LemmingState
{
    public LemmingBlockingState()
        : base()
    {
        AnimationName = "blocking";
    }

    public override void Enter()
    {
        base.Enter();
        Agent.MovementController.SetDirection(Direction.None);
        Agent.MovementController.SetForwardDirection(Agent.StateController.BlockingDirection);
        Agent.MovementController.OnArrivedAndRotated += activateLemmingAction;
    }

    public void activateLemmingAction()
    {
        Agent.MovementController.OnArrivedAndRotated -= activateLemmingAction;
        Agent.SetBlockActionActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        Agent.SetBlockActionActive(false);
    }
}
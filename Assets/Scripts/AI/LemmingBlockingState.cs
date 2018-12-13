using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingBlockingState : LemmingState
{
    public override void Enter()
    {
        base.Enter();
        Agent.AnimationController.setBool("Blocking", true);
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
        Agent.AnimationController.setBool("Blocking", false);
        Agent.SetBlockActionActive(false);
        base.Exit();
    }
}
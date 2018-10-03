using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachines;

public class LemmingState : State<LemmingAI>
{
    protected string AnimationName { get; set; }

    public override void Enter()
    {
        base.Enter();
        if(AnimationName != "")
        {
            Agent.Animator.Play(AnimationName);
        }

        Agent.MovementController.OnArrived += OnArrivedAtWaypoint;
    }

    public override void Update()
    {
        base.Update();
    }

    public virtual void OnArrivedAtWaypoint()
    {

    }

    public override void Exit()
    {
        base.Exit();
        Agent.MovementController.OnArrived -= OnArrivedAtWaypoint;
    }
}
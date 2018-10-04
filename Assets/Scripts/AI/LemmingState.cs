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

       // Agent.MovementController.OnGetNextWaypoint += OnGetNextWaypoint;
    }

    public override void TriggerEvent(int triggerEvent)
    {
        base.TriggerEvent(triggerEvent);
        if(triggerEvent == (int) LemmingAI.Trigger.GetNextWaypoint)
        {
            OnGetNextWaypoint();
        }
    }
    public override void Update()
    {
        base.Update();
    }

    public virtual void OnGetNextWaypoint()
    {

    }

    public override void Exit()
    {
        base.Exit();
       // Agent.MovementController.OnArrived -= OnGetNextWaypoint;
    }
}
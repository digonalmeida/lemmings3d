using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachines;

public class LemmingState : State<LemmingAI>
{
    private string animationName = "";
    public string AnimationName { get; set; }

    public override void Enter()
    {
        base.Enter();
        if(animationName != "")
        {
            Agent.Animator.Play(animationName);
        }
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingDeathState : LemmingState
{
    public LemmingDeathState()
        : base()
    {
        AnimationName = "falling";
        //TODO
    }

    public override void Enter()
    {
        base.Enter();
        Agent.GetComponent<LemmingStateController>().killLemming(); //Must change this to complete animation first!
    }

    public override void OnGetNextWaypoint()
    {
        base.OnGetNextWaypoint();
        //TODO
    }
}

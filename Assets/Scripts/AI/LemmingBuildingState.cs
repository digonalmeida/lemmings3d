using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingBuildingState : LemmingState
{
    public override void Enter()
    {
        base.Enter();
        Agent.MovementController.SetDirection(Direction.None);
        Agent.AnimationController.finishedAnimationAction += finishedAnimation;
    }

    public override void Exit()
    {
        base.Exit();
        Agent.MovementController.SetDirectionForward();
        Agent.AnimationController.finishedAnimationAction -= finishedAnimation;
    }

    public void finishedAnimation()
    {
        //Spawn Stairs
        //TODO
        //ControllerManager.Instance.mapController.AddBlock(Vector3Int.FloorToInt(Agent.transform.position + Directions.GetWorldDirection(Agent.MovementController.getForwardDirection())), ????);
        Agent.StateController.dequeueSkill();
        this.StateMachine.TriggerEvent((int)LemmingAI.Trigger.FinishedTask);
    }
}

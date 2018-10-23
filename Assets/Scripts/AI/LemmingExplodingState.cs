using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingExplodingState : LemmingState
{
    public LemmingExplodingState()
        : base()
    {
        AnimationName = "exploding";
    }

    public override void Enter()
    {
        base.Enter();
        Agent.MovementController.SetDirection(Direction.None);
        Agent.GetComponent<HighlightableObject>().canBeHighlighted = false;
    }

    public override void Update()
    {
        base.Update();
        if (Agent.AnimationController.isEndOfAnimation("exploding"))
        {
            ControllerManager.Instance.mapController.EraseWall(Vector3Int.RoundToInt(Agent.transform.position + Vector3.down));
            ControllerManager.Instance.mapController.EraseWall(Vector3Int.RoundToInt(Agent.transform.position + Vector3.up));
            ControllerManager.Instance.mapController.EraseWall(Vector3Int.RoundToInt(Agent.transform.position + Vector3.left));
            ControllerManager.Instance.mapController.EraseWall(Vector3Int.RoundToInt(Agent.transform.position + Vector3.right));
            ControllerManager.Instance.mapController.EraseWall(Vector3Int.RoundToInt(Agent.transform.position + Vector3.forward));
            ControllerManager.Instance.mapController.EraseWall(Vector3Int.RoundToInt(Agent.transform.position + Vector3.back));
            Agent.StateController.killLemming();
        }
    }
}

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
            LevelMap.MapController.Instance.EraseWall(Vector3Int.RoundToInt(Agent.transform.position + Vector3.down));
            LevelMap.MapController.Instance.EraseWall(Vector3Int.RoundToInt(Agent.transform.position + Vector3.up));
            LevelMap.MapController.Instance.EraseWall(Vector3Int.RoundToInt(Agent.transform.position + Vector3.left));
            LevelMap.MapController.Instance.EraseWall(Vector3Int.RoundToInt(Agent.transform.position + Vector3.right));
            LevelMap.MapController.Instance.EraseWall(Vector3Int.RoundToInt(Agent.transform.position + Vector3.forward));
            LevelMap.MapController.Instance.EraseWall(Vector3Int.RoundToInt(Agent.transform.position + Vector3.back));
            Agent.LemmingActions.KillLemming();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using FiniteStateMachines;
using UnityEngine;

public class LemmingAI : MonoBehaviour
{
    [SerializeField]
    private FiniteStateMachine<LemmingAI> stateMachine;

    [SerializeField]
    private Animator animator = null;

    private LemmingSimpleMovementController movementController;
    
    public enum Trigger
    {
        StartGame
    }

    public Animator Animator
    {
        get
        {
            return animator;
        }
    }
    
    public LemmingSimpleMovementController MovementController
    {
        get
        {
            return movementController;
        }
    }

    public LemmingState IdleState { get; private set; }

    public LemmingState WalkingState { get; private set; }

    public LemmingState DeadState { get; private set; }

    private void Awake()
    {
        movementController = GetComponent<LemmingSimpleMovementController>();
        stateMachine = new FiniteStateMachine<LemmingAI>(this);

        IdleState.AddTrigger((int)Trigger.StartGame, WalkingState);

        stateMachine.SetState(IdleState);
    }

    private void Update()
    {
        stateMachine.Update();
    }
}
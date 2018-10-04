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
    private LemmingStateController stateController;
    
    public enum Trigger
    {
        StartGame,
        ArrivedAtWaypoint,
        GetNextWaypoint
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

    public LemmingStateController StateController
    {
        get
        {
            return stateController;
        }
    }

    public LemmingState IdleState { get; private set; }

    public LemmingWalkingState WalkingState { get; private set; }

    public LemmingFallingState FallingState { get; private set; }

    public LemmingClimbingState ClimbingState { get; private set; }

    public LemmingState DeadState { get; private set; }

    private void Awake()
    {
        movementController = GetComponent<LemmingSimpleMovementController>();
        movementController.OnArrived += OnArrivetAtWaypoint;
        movementController.OnGetNextWaypoint += OnGetNextWaypoint;

        stateController = GetComponent<LemmingStateController>();
        stateMachine = new FiniteStateMachine<LemmingAI>(this);

        IdleState = new LemmingState();
        WalkingState = new LemmingWalkingState();
        FallingState = new LemmingFallingState();
        ClimbingState = new LemmingClimbingState();

        IdleState.AddTrigger((int)Trigger.StartGame, WalkingState);
        WalkingState.AddTransition((int)Trigger.ArrivedAtWaypoint, CheckClimb, ClimbingState);
        WalkingState.AddTransition((int)Trigger.ArrivedAtWaypoint, () => !movementController.CheckFloor(), FallingState);

        FallingState.AddTransition((int)Trigger.ArrivedAtWaypoint, () => movementController.CheckFloor(), WalkingState);

        ClimbingState.AddTransition((int)Trigger.ArrivedAtWaypoint, () => movementController.CheckFloor(), WalkingState);

        stateMachine.SetState(IdleState);
    }

    private void OnDestroy()
    {
        movementController.OnArrived -= OnArrivetAtWaypoint;
        movementController.OnGetNextWaypoint -= OnGetNextWaypoint;
    }

    private bool CheckClimb()
    {
        bool b = movementController.CheckWallForward() && stateController.checkSkill(Skill.Climber);
        return b;
    }
    private void OnArrivetAtWaypoint()
    {
        stateMachine.TriggerEvent((int)Trigger.ArrivedAtWaypoint);
    }

    private void OnGetNextWaypoint()
    {
        stateMachine.TriggerEvent((int)Trigger.GetNextWaypoint);
    }

    private void Start()
    {
        stateMachine.TriggerEvent((int)Trigger.StartGame);
    }

    private void Update()
    {
        stateMachine.Update();
    }
}
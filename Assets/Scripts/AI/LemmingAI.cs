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

    [SerializeField]
    private GameObject actionObject = null;

    private LemmingSimpleMovementController movementController;
    private LemmingStateController stateController;
    private LemmingState idleState = new LemmingState();
    private LemmingWalkingState walkingState = new LemmingWalkingState();
    private LemmingFallingState fallingState = new LemmingFallingState();
    private LemmingClimbingState climbingState = new LemmingClimbingState();
    private LemmingBlockingState blockingState = new LemmingBlockingState();

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

    public void SetBlockActionActive(bool actionActive)
    {
        if (actionObject == null)
        {
            return;
        }

        actionObject.SetActive(actionActive);
    }

    private void Awake()
    {
        movementController = GetComponent<LemmingSimpleMovementController>();
        movementController.OnArrived += OnArrivetAtWaypoint;
        movementController.OnGetNextWaypoint += OnGetNextWaypoint;

        stateController = GetComponent<LemmingStateController>();

        SetupStateMachine();
        
    }

    private void Start()
    {
        stateMachine.TriggerEvent((int)Trigger.StartGame);
    }

    public void SetupStateMachine()
    {
        stateMachine = new FiniteStateMachine<LemmingAI>(this);

        idleState.AddTrigger((int)Trigger.StartGame, walkingState);

        walkingState.AddTransition((int)Trigger.ArrivedAtWaypoint, CheckIsBlocker, blockingState);
        walkingState.AddTransition((int)Trigger.ArrivedAtWaypoint, CheckClimb, climbingState);
        walkingState.AddTransition((int)Trigger.ArrivedAtWaypoint, () => !movementController.CheckFloor(), fallingState);

        fallingState.AddTransition((int)Trigger.ArrivedAtWaypoint, () => movementController.CheckFloor(), walkingState);

        climbingState.AddTransition((int)Trigger.ArrivedAtWaypoint, () => movementController.CheckFloor(), walkingState);

        stateMachine.SetState(idleState);

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

    private bool CheckIsBlocker()
    {
        return stateController.checkIsBlocker();
    }

    private void OnArrivetAtWaypoint()
    {
        if(!enabled)
        {
            return;
        }
        stateMachine.TriggerEvent((int)Trigger.ArrivedAtWaypoint);
    }

    private void OnGetNextWaypoint()
    {
        if (!enabled)
        {
            return;
        }
        stateMachine.TriggerEvent((int)Trigger.GetNextWaypoint);
    }

    private void Update()
    {
        if(!enabled)
        {
            return;
        }

        stateMachine.Update();
    }
}
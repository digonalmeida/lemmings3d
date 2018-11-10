using System.Collections;
using System.Collections.Generic;
using FiniteStateMachines;
using UnityEngine;

public class LemmingAI : MonoBehaviour
{
    [SerializeField]
    private FiniteStateMachine<LemmingAI> stateMachine;

    private LemmingAnimationController lemmingAnimationController;

    [SerializeField]
    private LemmingActions lemmingActions;

    [SerializeField]
    private GameObject actionObject = null;

    private LemmingMovementController movementController;
    private LemmingStateController stateController;
    private LemmingState idleState = new LemmingState();
    private LemmingWalkingState walkingState = new LemmingWalkingState();
    private LemmingFallingState fallingState = new LemmingFallingState();
    private LemmingClimbingState climbingState = new LemmingClimbingState();
    private LemmingBlockingState blockingState = new LemmingBlockingState();
    private LemmingFloatingState floatingState = new LemmingFloatingState();
    private LemmingDeathState deathState = new LemmingDeathState();
    private LemmingDiggingState diggingState = new LemmingDiggingState();
    private LemmingBashingState bashingState = new LemmingBashingState();
    private LemmingExplodingState explodingState = new LemmingExplodingState();
    private LemmingBuildingState buildingState = new LemmingBuildingState();
    private LemmingExitingLevelState exitingLevelState = new LemmingExitingLevelState();

    public enum Trigger
    {
        StartGame,
        ArrivedAtExit,
        ArrivedAtWaypoint,
        GetNextWaypoint,
        FinishedTask
    }

    public LemmingActions LemmingActions {
        get
        {
            return lemmingActions;
        }
    }

    public LemmingAnimationController AnimationController
    {
        get
        {
            return lemmingAnimationController;
        }
    }

    public LemmingMovementController MovementController
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
        lemmingAnimationController = GetComponent<LemmingAnimationController>();
        movementController = GetComponent<LemmingMovementController>();
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

        walkingState.AddTransition((int)Trigger.ArrivedAtExit, () => true, exitingLevelState);
        walkingState.AddTransition((int)Trigger.ArrivedAtWaypoint, () => !movementController.CheckFloor(), fallingState);
        walkingState.AddTransition((int)Trigger.ArrivedAtWaypoint, CheckIsBlocker, blockingState);
        walkingState.AddTransition((int)Trigger.ArrivedAtWaypoint, () => movementController.CheckWallForward() && stateController.checkSkill(Skill.Basher), bashingState);
        walkingState.AddTransition((int)Trigger.ArrivedAtWaypoint, () => movementController.CheckWallForward() && stateController.isClimber(), climbingState);
        walkingState.AddTransition((int)Trigger.ArrivedAtWaypoint, () => stateController.checkSkill(Skill.Digger), diggingState);
        walkingState.AddTransition((int)Trigger.ArrivedAtWaypoint, () => stateController.checkSkill(Skill.Exploder), explodingState);
        walkingState.AddTransition((int)Trigger.ArrivedAtWaypoint, () => stateController.checkSkill(Skill.Builder), buildingState);

        fallingState.AddTransition((int)Trigger.ArrivedAtWaypoint, () => movementController.CheckFloor() && movementController.checkFallDeath(), deathState);
        fallingState.AddTransition((int)Trigger.ArrivedAtWaypoint, () => movementController.CheckFloor(), walkingState);
        fallingState.AddTransition((int)Trigger.ArrivedAtWaypoint, () => stateController.isFloater(), floatingState);

        floatingState.AddTransition((int)Trigger.ArrivedAtWaypoint, () => movementController.CheckFloor(), walkingState);

        climbingState.AddTransition((int)Trigger.ArrivedAtWaypoint, () => movementController.CheckFloor(), walkingState);

        diggingState.AddTransition((int)Trigger.FinishedTask, () => true, walkingState);
        bashingState.AddTransition((int)Trigger.FinishedTask, () => true, walkingState);
        buildingState.AddTransition((int)Trigger.FinishedTask, () => true, walkingState);

        stateMachine.SetState(idleState);

    }

    private void OnDestroy()
    {
        movementController.OnArrived -= OnArrivetAtWaypoint;
        movementController.OnGetNextWaypoint -= OnGetNextWaypoint;
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
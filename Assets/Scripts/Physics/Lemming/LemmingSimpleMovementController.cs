using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LemmingStateController))]
public class LemmingSimpleMovementController : MonoBehaviour
{
    //Internal Variables
    [SerializeField]
    private float movementSpeed = 3;

    [SerializeField]
    private float gravitySpeed = 0;

    [SerializeField]
    private float gravityAcceleration = 10;

    [SerializeField]
    private float turningRate = 7;

    [SerializeField]
    private bool climb;

    private LayerMask wallsLayerMask;
    private LayerMask lemmingsActionLayerMask;
    private RaycastHit[] raycastHits;
    private Collider[] overlapSphereHits;

    private bool climbing;
    private bool stopped = false;
    private bool arrived = false;

    //Reference Variables
    private LemmingStateController lemmingStateController;
    private LemmingActions lemmingActions;
    private Vector3 nextWaypoint;

    [SerializeField]
    private Vector3Int targetPositionAddress;

    [SerializeField]
    private Corner currentCorner;
    
    [SerializeField]
    private Direction movementDirection;

    [SerializeField]
    private Direction facingDirection;

    public enum Corner
    {
        Center,
        NorthEast,
        NorthWest,
        SouthEast,
        SouthWest
    }

    public Action OnArrived;
    public Action OnGetNextWaypoint;

    private void Start ()
    {
        //Initialize Variables
        targetPositionAddress = Vector3Int.RoundToInt(transform.position);
        nextWaypoint = this.transform.position;
        lemmingStateController = this.GetComponent<LemmingStateController>();
        lemmingActions = GetComponent<LemmingActions>();
        movementDirection = Direction.West;
        wallsLayerMask = LayerMask.GetMask("Wall");
        lemmingsActionLayerMask = LayerMask.GetMask("LemmingAction");
        raycastHits = new RaycastHit[1];
        overlapSphereHits = new Collider[1];
    }

    public Vector3 GetTargetPosition(Vector3Int centerPos, Corner corner)
    {
        Vector3 directionVec = new Vector3();
        switch (corner)
        {
            case Corner.Center:
                directionVec = Vector3.zero;
                break;
            case Corner.NorthEast:
                directionVec = Directions.North + Directions.East;
                break;
            case Corner.NorthWest:
                directionVec = Directions.North + Directions.West;
                break;
            case Corner.SouthEast:
                directionVec = Directions.South + Directions.East;
                break;
            case Corner.SouthWest:
                directionVec = Directions.South + Directions.West;
                break;
            default:
                break;
        }

        float distance = 0.5f;
        var distanceVec = directionVec.normalized * distance;
        Vector3 pos = centerPos + distanceVec;
        
        return pos;
    }

    public bool CheckFloor()
    {
        int hits = Physics.RaycastNonAlloc(this.transform.position, Vector3.down, raycastHits, 1f, wallsLayerMask);
        return hits > 0;
    }

    public bool TrySetWaypointExit()
    {
        int hits = Physics.OverlapSphereNonAlloc(nextWaypoint, 0.1f, overlapSphereHits, lemmingsActionLayerMask);
        for (int i = 0; i < hits; i++)
        {
            var hit = overlapSphereHits[i];

            ExitPoint exitPoint = hit.GetComponentInParent<ExitPoint>();
            if (exitPoint != null)
            {
                SetWaypontExit(exitPoint);
                return true;
            }
        }
        return false;
    }

    public bool TrySetWaypointLemmingAction()
    {
        int hits = Physics.OverlapSphereNonAlloc(nextWaypoint, 0.1f, overlapSphereHits, lemmingsActionLayerMask);
        for (int i = 0; i < hits; i++)
        {
            var hit = overlapSphereHits[i];

            LemmingStateController otherLemmingStateController = hit.GetComponentInParent<LemmingStateController>();

            if (otherLemmingStateController != null)
            {
                if (otherLemmingStateController.checkIsBlocker())
                {
                    var direction = otherLemmingStateController.BlockingDirection;
                    if (movementDirection != direction)
                    {
                        movementDirection = direction;
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public void SetFacingDirection(Direction direction)
    {
        facingDirection = direction;
    }

    public Direction CheckChangeDirectionOrders()
    {
        int hits = Physics.OverlapSphereNonAlloc(transform.position, 0.5f, overlapSphereHits, lemmingsActionLayerMask);
        for (int i = 0; i < hits; i++)
        {
            var hit = overlapSphereHits[i];

            LemmingStateController otherLemmingStateController = hit.GetComponentInParent<LemmingStateController>();

            if (otherLemmingStateController != null)
            {
                if (otherLemmingStateController.checkIsBlocker())
                {
                    var direction = otherLemmingStateController.BlockingDirection;
                    return direction;
                }
            }
        }

        return Direction.None;
    }

    public bool CheckWallForward()
    {
        Debug.DrawRay(transform.position, Directions.GetWorldDirection(facingDirection));
        var hits = Physics.RaycastNonAlloc(this.transform.position, Directions.GetWorldDirection(facingDirection), raycastHits, 0.45f, wallsLayerMask);
        return hits > 0;
    }
    
    public void SetWaypointForward()
    {
        climbing = false;
        movementDirection = facingDirection;
    }

    public void SetWaypointTurnAround()
    {
        movementDirection = Directions.getOppositeDirection(movementDirection);
    }

    public void SetWaypointClimbing()
    {
        climbing = true;
        movementDirection = Direction.Up;
    }

    public void SetWaypontExit(ExitPoint exitPoint)
    {
        nextWaypoint = exitPoint.exitLemmingFinalTransform.position;
        stopped = true;
        lemmingActions.EnterExitPoint();
    }

    public void SetWaypointFalling()
    {
        movementDirection = Direction.Down;
    }

    private void ApplyGravity()
    {
        if(movementDirection != Direction.Down)
        {
            gravitySpeed = 0;
        }
        else
        {
            gravitySpeed += gravityAcceleration * Time.fixedDeltaTime;
        }
        
    }

    private void FixedUpdate()
    {
        if (stopped)
        {
            return;
        }

        if (!enabled)
        {
            return;
        }
        
        if(arrived)
        {
            if (OnGetNextWaypoint != null)
            {
                OnGetNextWaypoint();
            }

            UpdateWaypoint(movementDirection);
            arrived = false;
        }
        var pos = GetTargetPosition(targetPositionAddress, currentCorner);
        ApplyGravity();
        transform.position = Vector3.MoveTowards(this.transform.position, pos, (movementSpeed + gravitySpeed) * Time.fixedDeltaTime);

        var t = Vector3.RotateTowards(transform.forward, Directions.GetWorldDirection(facingDirection), turningRate * Time.fixedDeltaTime, 100);

        transform.forward = t;
        
        if ((pos - this.transform.position) == Vector3.zero)
        {
            arrived = true;
            if (OnArrived != null)
            {
                OnArrived();
            }
           // Debug.Break();
        }
    }

    public bool TryMoveInCorners(Direction direction)
    {
        Corner[] directionCorners;

        switch (direction)
        {
            case Direction.North:
                directionCorners = new Corner[] { Corner.SouthEast, Corner.NorthEast };
                break;
            case Direction.East:
                directionCorners = new Corner[] { Corner.SouthWest, Corner.SouthEast };
                break;
            case Direction.South:
                directionCorners = new Corner[] { Corner.NorthWest, Corner.SouthWest };
                break;
            case Direction.West:
                directionCorners = new Corner[] { Corner.NorthEast, Corner.NorthWest };
                break;
            case Direction.Up:
                return false;
            case Direction.Down:
                return false;
            case Direction.None:
                return false;
            default:
                return false;
        }

        if(directionCorners.Length < 1)
        {
            return false;
        }

        for (int i = 0; i < directionCorners.Length; i++)
        {
            if(directionCorners[i] == currentCorner)
            {
                int nextI = i + 1;
                if (nextI >= directionCorners.Length)
                {
                    //set first corner in next waypoint
                    currentCorner = directionCorners[0];
                    return false;
                }
                else
                {
                    currentCorner = directionCorners[nextI];
                    return true;
                }
            }
        }

        //not in a valid corner for this direction. rotate to it.

        currentCorner = RotateCorner(currentCorner, directionCorners[0]);
        return true;
    }

    private bool UpdateCorner(Direction newDirection)
    {
        Corner targetCorner = currentCorner;

        switch (newDirection)
        {
            case Direction.North:
                targetCorner = Corner.NorthWest;
                break;
            case Direction.East:
                targetCorner = Corner.SouthEast;
                break;
            case Direction.South:
                targetCorner = Corner.SouthWest;
                break;
            case Direction.West:
                targetCorner = Corner.NorthWest;
                break;
            case Direction.Up:
                targetCorner = currentCorner;
                break;
            case Direction.Down:
                targetCorner = currentCorner;
                break;
            case Direction.None:
                targetCorner = Corner.Center;
                break;
            default:
                break;
        }

        if(targetCorner == currentCorner)
        {
            return true;
        }

        currentCorner = RotateCorner(currentCorner, targetCorner);
        
        return false;
    }

    private Corner RotateCorner(Corner from, Corner to)
    {
        if (from == to)
        {
            return from;
        }

        if (to == Corner.Center)
        {
            return Corner.Center;
        }

        switch (from)
        {
            case Corner.Center:
                return to;
            case Corner.NorthEast:
                return Corner.NorthWest;
            case Corner.NorthWest:
                return Corner.SouthWest;
            case Corner.SouthEast:
                return Corner.NorthEast;
            case Corner.SouthWest:
                return Corner.SouthEast;
            default:
                return Corner.Center;
        }
    }
    
    private void UpdateWaypoint(Direction newDirection)
    {
        if (newDirection != Direction.Up && newDirection != Direction.Down)
        {
            facingDirection = newDirection;
        }

        if (!TryMoveInCorners(newDirection))
        {
            targetPositionAddress += Vector3Int.RoundToInt(Directions.GetWorldDirection(newDirection));
        }
        
        return;
    }

}

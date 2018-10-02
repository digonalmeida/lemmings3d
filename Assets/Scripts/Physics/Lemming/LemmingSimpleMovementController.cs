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
    private bool climb;

    private LayerMask wallsLayerMask;
    private LayerMask lemmingsActionLayerMask;
    private RaycastHit[] raycastHits;
    private Collider[] overlapSphereHits;

    private bool climbing;
    private bool stopped = false;
    
    //Reference Variables
    private LemmingStateController lemmingStateController;
    private LemmingActions lemmingActions;
    private Vector3 nextWaypoint;

    [SerializeField]
    private Direction movementDirection;

    private void Start ()
    {
        //Initialize Variables
        nextWaypoint = this.transform.position;
        lemmingStateController = this.GetComponent<LemmingStateController>();
        lemmingActions = GetComponent<LemmingActions>();
        movementDirection = Direction.West;
        wallsLayerMask = LayerMask.GetMask("Wall");
        lemmingsActionLayerMask = LayerMask.GetMask("LemmingAction");
        raycastHits = new RaycastHit[1];
        overlapSphereHits = new Collider[1];
    }

    private void setNewDirection(Direction newDirection)
    {
        if(newDirection != movementDirection)
        {
            updateWaypoint(newDirection);
            movementDirection = newDirection;
        }
    }

    private bool CheckFloor()
    {
        int hits = Physics.RaycastNonAlloc(this.transform.position, Vector3.down, raycastHits, 1f, wallsLayerMask);
        return hits > 0;
    }

    private bool TrySetWaypointExit()
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

    private bool TrySetWaypointLemmingAction()
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
                        setNewDirection(direction);
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private bool CheckWallForward()
    {
        var hits = Physics.RaycastNonAlloc(this.transform.position, Directions.GetWorldDirection(movementDirection), raycastHits, 0.45f, wallsLayerMask);
        return hits > 0;
    }

    private void CalculateNextWaypoint()
    {
        if (!CheckFloor() && !climbing)
        {
            SetNextWaypointFalling();
            return;
        }

        if (lemmingStateController.checkMovementBlockingSkills())
        {
            return;
        }

        if (TrySetWaypointExit())
        {
            return;
        }

        if (TrySetWaypointLemmingAction())
        {
            return;
        }
        
        if (CheckWallForward())
        {
            if (climb)
            {
                SetNextWaypointClimbing();
            }
            else
            {
                SetNextWaypointTurnAround();
            }
            return;
        }

        SetWaypointForward();
    }

    private void SetWaypointForward()
    {
        climbing = false;
        updateWaypoint(movementDirection);
    }

    private void SetNextWaypointTurnAround()
    {
        //Turn Around
        if (movementDirection == Direction.North) setNewDirection(Direction.South);
        else if (movementDirection == Direction.East) setNewDirection(Direction.West);
        else if (movementDirection == Direction.South) setNewDirection(Direction.North);
        else if (movementDirection == Direction.West) setNewDirection(Direction.East);
    }

    private void SetNextWaypointClimbing()
    {
        climbing = true;
        nextWaypoint.Set(nextWaypoint.x, nextWaypoint.y + 1f, nextWaypoint.z);
    }

    private void SetWaypontExit(ExitPoint exitPoint)
    {
        nextWaypoint = exitPoint.exitLemmingFinalTransform.position;
        stopped = true;
        lemmingActions.EnterExitPoint();
    }

    private void SetNextWaypointFalling()
    {
        nextWaypoint.Set(nextWaypoint.x, nextWaypoint.y - 1f, nextWaypoint.z);
    }

    private void FixedUpdate()
    {
        if (stopped)
        {
            return;
        }

        if ((nextWaypoint - this.transform.position) == Vector3.zero)
        {
            CalculateNextWaypoint();
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, nextWaypoint, movementSpeed * Time.deltaTime);
            transform.forward = Vector3.Lerp(transform.forward, Directions.GetWorldDirection(movementDirection), 7 * Time.deltaTime);
        }
    }

    private void updateWaypoint(Direction newDirection)
    {
        if (newDirection != movementDirection)
        {
            if (movementDirection == Direction.North)
            {
                if (newDirection == Direction.South) nextWaypoint.Set(nextWaypoint.x - 0.6f, nextWaypoint.y, nextWaypoint.z);
                else if (newDirection == Direction.West) nextWaypoint.Set(nextWaypoint.x, nextWaypoint.y, nextWaypoint.z + 0.6f);
            }
            else if (movementDirection == Direction.East)
            {
                if (newDirection == Direction.West) nextWaypoint.Set(nextWaypoint.x, nextWaypoint.y, nextWaypoint.z + 0.6f);
                else if (newDirection == Direction.North) nextWaypoint.Set(nextWaypoint.x + 0.6f, nextWaypoint.y, nextWaypoint.z);
            }
            else if (movementDirection == Direction.South)
            {
                if (newDirection == Direction.North) nextWaypoint.Set(nextWaypoint.x + 0.6f, nextWaypoint.y, nextWaypoint.z);
                else if (newDirection == Direction.East) nextWaypoint.Set(nextWaypoint.x, nextWaypoint.y, nextWaypoint.z - 0.6f);
            }
            else if (movementDirection == Direction.West)
            {
                if (newDirection == Direction.East) nextWaypoint.Set(nextWaypoint.x, nextWaypoint.y, nextWaypoint.z - 0.6f);
                else if (newDirection == Direction.South) nextWaypoint.Set(nextWaypoint.x - 0.6f, nextWaypoint.y, nextWaypoint.z);
            }
        }
        //Continue Movement
        else
        {
            if (movementDirection == Direction.North)
            {
                if (nextWaypoint.z < 0f)
                {
                    if (System.Math.Round(Mathf.Abs(nextWaypoint.z % 1), 2) == 0.3) nextWaypoint.Set(nextWaypoint.x, nextWaypoint.y, nextWaypoint.z + 0.4f);
                    else nextWaypoint.Set(nextWaypoint.x, nextWaypoint.y, nextWaypoint.z + 0.6f);
                }
                else
                {
                    if (System.Math.Round(Mathf.Abs(nextWaypoint.z % 1), 2) == 0.7) nextWaypoint.Set(nextWaypoint.x, nextWaypoint.y, nextWaypoint.z + 0.4f);
                    else nextWaypoint.Set(nextWaypoint.x, nextWaypoint.y, nextWaypoint.z + 0.6f);
                }
            }
            else if (movementDirection == Direction.East)
            {
                if (nextWaypoint.x < 0f)
                {
                    if (System.Math.Round(Mathf.Abs(nextWaypoint.x % 1), 2) == 0.3) nextWaypoint.Set(nextWaypoint.x + 0.6f, nextWaypoint.y, nextWaypoint.z);
                    else nextWaypoint.Set(nextWaypoint.x + 0.4f, nextWaypoint.y, nextWaypoint.z);
                }
                else
                {
                    if (System.Math.Round(Mathf.Abs(nextWaypoint.x % 1), 2) == 0.7) nextWaypoint.Set(nextWaypoint.x + 0.6f, nextWaypoint.y, nextWaypoint.z);
                    else nextWaypoint.Set(nextWaypoint.x + 0.4f, nextWaypoint.y, nextWaypoint.z);
                }
            }
            else if (movementDirection == Direction.South)
            {
                if (nextWaypoint.z < 0f)
                {
                    if (System.Math.Round(Mathf.Abs(nextWaypoint.z % 1), 2) == 0.3) nextWaypoint.Set(nextWaypoint.x, nextWaypoint.y, nextWaypoint.z - 0.6f);
                    else nextWaypoint.Set(nextWaypoint.x, nextWaypoint.y, nextWaypoint.z - 0.4f);
                }
                else
                {
                    if (System.Math.Round(Mathf.Abs(nextWaypoint.z % 1), 2) == 0.7) nextWaypoint.Set(nextWaypoint.x, nextWaypoint.y, nextWaypoint.z - 0.6f);
                    else nextWaypoint.Set(nextWaypoint.x, nextWaypoint.y, nextWaypoint.z - 0.4f);
                }
            }
            else if (movementDirection == Direction.West)
            {
                if (nextWaypoint.x < 0f)
                {
                    if (System.Math.Round(Mathf.Abs(nextWaypoint.x % 1), 2) == 0.3) nextWaypoint.Set(nextWaypoint.x - 0.4f, nextWaypoint.y, nextWaypoint.z);
                    else nextWaypoint.Set(nextWaypoint.x - 0.6f, nextWaypoint.y, nextWaypoint.z);
                }
                else
                {
                    if (System.Math.Round(Mathf.Abs(nextWaypoint.x % 1), 2) == 0.7) nextWaypoint.Set(nextWaypoint.x - 0.4f, nextWaypoint.y, nextWaypoint.z);
                    else nextWaypoint.Set(nextWaypoint.x - 0.6f, nextWaypoint.y, nextWaypoint.z);
                }
            }
        }
    }
}

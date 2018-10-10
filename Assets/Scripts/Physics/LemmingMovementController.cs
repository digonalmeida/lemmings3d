using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LemmingStateController))]
public class LemmingMovementController : MonoBehaviour
{
    //Internal Variables
    private float movementSpeed = 3;
    private LayerMask wallsLayerMask;
    private LayerMask lemmingsActionLayerMask;
    private RaycastHit[] raycastHits;
    private Collider[] overlapSphereHits;
    private bool queuedChangeBehavior;

    //Reference Variables
    private LemmingStateController lemmingStateController;
    private LemmingActions lemmingActions;
    private Vector3 nextWaypoint;
    private Vector3 movementDirection;

    //Start Method
    void Start ()
    {
        //Initialize Variables
        lemmingStateController = this.GetComponent<LemmingStateController>();
        lemmingActions = GetComponent<LemmingActions>();
        wallsLayerMask = LayerMask.GetMask("Wall");
        lemmingsActionLayerMask = LayerMask.GetMask("LemmingAction");
        raycastHits = new RaycastHit[1];
        overlapSphereHits = new Collider[1];
        queuedChangeBehavior = false;
    }

    //Set Initial Movement
    public void setInitialMovementVariables(Vector3 initialPosition, Vector3 movementDirection)
    {
        this.transform.position = initialPosition;
        nextWaypoint = this.transform.position;
        this.movementDirection = movementDirection;
    }

    //Set Direction
    public void setNewDirection(Vector3 newDirection)
    {
        if(newDirection != movementDirection)
        {
            updateWaypoint(newDirection);
            movementDirection = newDirection;
        }
    }

    //Fixed Update Method
    private void FixedUpdate()
    {
        //Check Waypoint Distance
        if ((nextWaypoint - this.transform.position) == Vector3.zero)
        {
            //Check Floor
            int hits = Physics.RaycastNonAlloc(this.transform.position, Vector3.down, raycastHits, 1f, wallsLayerMask);
            if (hits == 0)
            {
                //nextWaypoint.Set(nextWaypoint.x, nextWaypoint.y - 1f, nextWaypoint.z);
                //Change Behavior to Falling!
            }
            else
            {
                //Check if there is a behavior queued
                if (queuedChangeBehavior)
                {
                    //Change Behavior to Specific Skill!
                }
                else
                {
                    //Check Next Enqueued Skills
                    Skill enqueuedSkill = lemmingStateController.checkEnqueuedSkill();
                    if (enqueuedSkill != Skill.None)
                    {
                        //Move to Center
                        nextWaypoint = new Vector3(Mathf.FloorToInt(this.transform.position.x), this.transform.position.y, Mathf.FloorToInt(this.transform.position.z));
                        queuedChangeBehavior = true;
                    }

                    //Check if at Exit Point
                    hits = Physics.OverlapSphereNonAlloc(nextWaypoint, 0.1f, overlapSphereHits, lemmingsActionLayerMask);
                    for (int i = 0; i < hits; i++)
                    {
                        ExitPoint exitPoint = overlapSphereHits[i].GetComponentInParent<ExitPoint>();
                        if (exitPoint != null)
                        {
                            nextWaypoint = exitPoint.exitLemmingFinalTransform.position;
                            //stopped = true
                            //lemmingActions.EnterExitPoint();
                            //return;

                            //Change Behavior to Exiting Level!
                        }

                        //Check Other Lemmings Actions
                        LemmingStateController otherLemmingStateController = overlapSphereHits[i].GetComponentInParent<LemmingStateController>();
                        if (otherLemmingStateController != null)
                        {
                            Skill skill = otherLemmingStateController.checkEnqueuedSkill();
                            if (skill == Skill.Blocker_TurnNorth && movementDirection != Directions.North)
                            {
                                setNewDirection(Directions.North);
                                return;
                            }
                            else if (skill == Skill.Blocker_TurnEast && movementDirection != Directions.East)
                            {
                                setNewDirection(Directions.East);
                                return;
                            }
                            else if (skill == Skill.Blocker_TurnSouth && movementDirection != Directions.South)
                            {
                                setNewDirection(Directions.South);
                                return;
                            }
                            else if (skill == Skill.Blocker_TurnWest && movementDirection != Directions.West)
                            {
                                setNewDirection(Directions.West);
                                return;
                            }
                        }
                    }

                    //Check Walls
                    hits = Physics.RaycastNonAlloc(this.transform.position, movementDirection, raycastHits, 0.45f, wallsLayerMask);
                    if (hits == 1)
                    {
                        if (lemmingStateController.isClimber())
                        {
                            //Change Behavior to Climbing!
                            //nextWaypoint.Set(nextWaypoint.x, nextWaypoint.y + 1f, nextWaypoint.z);
                        }
                        else
                        {
                            //Turn Around
                            if (movementDirection == Directions.North) setNewDirection(Directions.South);
                            else if (movementDirection == Directions.East) setNewDirection(Directions.West);
                            else if (movementDirection == Directions.South) setNewDirection(Directions.North);
                            else if (movementDirection == Directions.West) setNewDirection(Directions.East);
                        }
                    }
                    else
                    {
                        //Move Normally
                        updateWaypoint(movementDirection);
                    }
                }
            }
        }

        //Move Lemming
        else this.transform.position = Vector3.MoveTowards(this.transform.position, nextWaypoint, movementSpeed * Time.deltaTime);
    }

    //Main Movement Logic
    private void updateWaypoint(Vector3 newDirection)
    {
        //Change Direction
        if (newDirection != movementDirection)
        {
            if (movementDirection == Directions.North)
            {
                if (newDirection == Directions.South) nextWaypoint.Set(nextWaypoint.x - 0.6f, nextWaypoint.y, nextWaypoint.z);
                else if (newDirection == Directions.West) nextWaypoint.Set(nextWaypoint.x, nextWaypoint.y, nextWaypoint.z + 0.6f);
            }
            else if (movementDirection == Directions.East)
            {
                if (newDirection == Directions.West) nextWaypoint.Set(nextWaypoint.x, nextWaypoint.y, nextWaypoint.z + 0.6f);
                else if (newDirection == Directions.North) nextWaypoint.Set(nextWaypoint.x + 0.6f, nextWaypoint.y, nextWaypoint.z);
            }
            else if (movementDirection == Directions.South)
            {
                if (newDirection == Directions.North) nextWaypoint.Set(nextWaypoint.x + 0.6f, nextWaypoint.y, nextWaypoint.z);
                else if (newDirection == Directions.East) nextWaypoint.Set(nextWaypoint.x, nextWaypoint.y, nextWaypoint.z - 0.6f);
            }
            else if (movementDirection == Directions.West)
            {
                if (newDirection == Directions.East) nextWaypoint.Set(nextWaypoint.x, nextWaypoint.y, nextWaypoint.z - 0.6f);
                else if (newDirection == Directions.South) nextWaypoint.Set(nextWaypoint.x - 0.6f, nextWaypoint.y, nextWaypoint.z);
            }
        }
        //Continue Movement
        else
        {
            if (movementDirection == Directions.North)
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
            else if (movementDirection == Directions.East)
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
            else if (movementDirection == Directions.South)
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
            else if (movementDirection == Directions.West)
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

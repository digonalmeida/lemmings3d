using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LemmingStateController))]
public class LemmingMovementController : MonoBehaviour
{
    //Internal Variables
    private float movementSpeed = 3;
    private LayerMask wallsLayerMask;
    private RaycastHit[] raycastHits;

    //Reference Variables
    private LemmingStateController lemmingStateController;
    [SerializeField]
    private Vector3 nextWaypoint;
    private Vector3 movementDirection;

    //Awake Method
    private void Awake()
    {
        lemmingStateController = this.GetComponent<LemmingStateController>();
    }

    //Start Method
    void Start ()
    {
        nextWaypoint = this.transform.position;
        movementDirection = Vector3.right;
        wallsLayerMask = LayerMask.GetMask("Wall");
        raycastHits = new RaycastHit[1];
    }

    //Set Direction
    public void setDirection(Vector3 newDirection)
    {
        movementDirection = newDirection;
    }

    //Fixed Update Method
    private void FixedUpdate()
    {
        if ((nextWaypoint - this.transform.position) == Vector3.zero)
        {
            //Check Floor
            int hits = Physics.RaycastNonAlloc(this.transform.position, Vector3.down, raycastHits, 1f, wallsLayerMask);
            if (hits == 0)
            {
                nextWaypoint.Set(this.transform.position.x, this.transform.position.y - 1f, this.transform.position.z);
                //TODO (Remove Skills, etc)
            }
            else
            {
                //Check Walls
                hits = Physics.RaycastNonAlloc(this.transform.position, movementDirection, raycastHits, 0.45f, wallsLayerMask);
                if (hits == 1)
                {
                    if (movementDirection == Vector3.right)
                    {
                        setDirection(Vector3.left);
                        nextWaypoint.Set(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.6f);
                    }
                    else if (movementDirection == Vector3.left)
                    {
                        setDirection(Vector3.right);
                        nextWaypoint.Set(this.transform.position.x, this.transform.position.y, this.transform.position.z + 0.6f);
                    }
                    else if (movementDirection == Vector3.forward)
                    {
                        setDirection(Vector3.back);
                        nextWaypoint.Set(this.transform.position.x - 0.6f, this.transform.position.y, this.transform.position.z);
                    }
                    else if (movementDirection == Vector3.back)
                    {
                        setDirection(Vector3.forward);
                        nextWaypoint.Set(this.transform.position.x + 0.6f, this.transform.position.y, this.transform.position.z);
                    }
                }
                else updateWaypoint();
            }
        }
        //Move Lemming
        else this.transform.position = Vector3.MoveTowards(this.transform.position, nextWaypoint, movementSpeed * Time.deltaTime);
    }

    //On Trigger Enter
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Box Collider Enter");
        //TODO: Lemming Actions
    }

    //Main Movement Logic
    private void updateWaypoint()
    {
        if (movementDirection == Vector3.right)
        {
            if (this.transform.position.x < 0f)
            {
                if(System.Math.Round(Mathf.Abs(this.transform.position.x % 1), 2) == 0.3) nextWaypoint.Set(this.transform.position.x + 0.6f, this.transform.position.y, this.transform.position.z);
                else nextWaypoint.Set(this.transform.position.x + 0.4f, this.transform.position.y, this.transform.position.z);
            }
            else
            {
                if (System.Math.Round(Mathf.Abs(this.transform.position.x % 1), 2) == 0.7) nextWaypoint.Set(this.transform.position.x + 0.6f, this.transform.position.y, this.transform.position.z);
                else nextWaypoint.Set(this.transform.position.x + 0.4f, this.transform.position.y, this.transform.position.z);
            }
        }
        else if (movementDirection == Vector3.left)
        {
            if (this.transform.position.x < 0f)
            {
                if (System.Math.Round(Mathf.Abs(this.transform.position.x % 1), 2) == 0.3) nextWaypoint.Set(this.transform.position.x - 0.4f, this.transform.position.y, this.transform.position.z);
                else nextWaypoint.Set(this.transform.position.x - 0.6f, this.transform.position.y, this.transform.position.z);
            }
            else
            {
                if (System.Math.Round(Mathf.Abs(this.transform.position.x % 1), 2) == 0.7) nextWaypoint.Set(this.transform.position.x - 0.4f, this.transform.position.y, this.transform.position.z);
                else nextWaypoint.Set(this.transform.position.x - 0.6f, this.transform.position.y, this.transform.position.z);
            }
        }
        else if (movementDirection == Vector3.forward)
        {
            if (this.transform.position.z < 0f)
            {
                if (System.Math.Round(Mathf.Abs(this.transform.position.z % 1), 2) == 0.3) nextWaypoint.Set(this.transform.position.x, this.transform.position.y, this.transform.position.z + 0.4f);
                else nextWaypoint.Set(this.transform.position.x, this.transform.position.y, this.transform.position.z + 0.6f);
            }
            else
            {
                if (System.Math.Round(Mathf.Abs(this.transform.position.z % 1), 2) == 0.7) nextWaypoint.Set(this.transform.position.x, this.transform.position.y, this.transform.position.z + 0.4f);
                else nextWaypoint.Set(this.transform.position.x, this.transform.position.y, this.transform.position.z + 0.6f);
            }
        }
        else if (movementDirection == Vector3.back)
        {
            if (this.transform.position.z < 0f)
            {
                if (System.Math.Round(Mathf.Abs(this.transform.position.z % 1), 2) == 0.3) nextWaypoint.Set(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.6f);
                else nextWaypoint.Set(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.4f);
            }
            else
            {
                if (System.Math.Round(Mathf.Abs(this.transform.position.z % 1), 2) == 0.7) nextWaypoint.Set(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.6f);
                else nextWaypoint.Set(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.4f);
            }
        }
    }
}

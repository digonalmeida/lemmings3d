using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class LemmingController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 3;

    [SerializeField]
    public RaycastSensor forwardSensor = new RaycastSensor();

    [SerializeField]
    public RaycastSensor rightSensor = new RaycastSensor();

    [SerializeField]
    public RaycastSensor leftSensor = new RaycastSensor();

    [SerializeField]
    public RaycastSensor floorForwardSensor = new RaycastSensor();

    [SerializeField]
    public RaycastSensor footForwardSensor = new RaycastSensor();

    public CharacterController CharacterController { get; private set; }
    public Vector3 MovementVelocity { get; set; }
    public Vector3 GravityVelocity { get; set; }
    public WanderBehaviour WanderBehaviour { get; private set; }
    public ClimbBehaviour ClimbBehaviour { get; private set; }
    public LemmingBehaviour CurrentBehaviour { get; set; }

    public float MovementSpeed
    {
        get
        {
            return movementSpeed;
        }
        set
        {
            movementSpeed = value;
        }
    }

    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        WanderBehaviour = new WanderBehaviour();
        ClimbBehaviour = new ClimbBehaviour();
    }

    private void Start()
    {
        CurrentBehaviour = new WanderBehaviour();
    }

    private void Update()
    {
        if (CurrentBehaviour != null)
        {
            CurrentBehaviour.Update(this);
        }
    }

    private void FixedUpdate()
    {
        GravityVelocity += Physics.gravity * Time.fixedDeltaTime;
        CharacterController.Move((MovementVelocity + GravityVelocity) * Time.fixedDeltaTime);
        if(CharacterController.isGrounded)
        {
            GravityVelocity = Vector3.zero;
        }
    }

    public void Jump()
    {
        GravityVelocity += Vector3.up * jumpForce;
    }

    private void OnDrawGizmosSelected()
    {
        forwardSensor.DrawGizmos(transform);
        rightSensor.DrawGizmos(transform);
        leftSensor.DrawGizmos(transform);
        floorForwardSensor.DrawGizmos(transform);
        footForwardSensor.DrawGizmos(transform);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotCameraMovement : MonoBehaviour
{
    public GameObject pivotObject = null;

    [SerializeField]
    private float speed = 50;

    [SerializeField]
    private float turningRate = 7000;

    [SerializeField]
    private float zoomSpeed = 1000;

    [SerializeField]
    private Vector3 pivotPosition = new Vector3();

    [SerializeField]
    private float distance = 1;

    [SerializeField]
    private Vector3 eulerRotation = new Vector3();
    
	private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            var rotation = transform.rotation;
            eulerRotation.x -= Input.GetAxis("Mouse Y") * turningRate * Time.deltaTime;
            eulerRotation.y += Input.GetAxis("Mouse X") * turningRate * Time.deltaTime;
            transform.rotation = rotation;
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 velocity = Vector3.zero;
            velocity += -transform.up * Input.GetAxis("Mouse Y");
            velocity += -transform.right * Input.GetAxis("Mouse X");
            pivotPosition += velocity * speed * Time.deltaTime;
            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pivotObject = null;
        }

        distance -= Time.deltaTime * zoomSpeed * Input.GetAxis("Mouse ScrollWheel");
        distance = Mathf.Max(distance, 1);

        var direction = Vector3.forward;
        direction = Quaternion.Euler(eulerRotation) * direction;
        var startPosition = pivotPosition;
        if (pivotObject != null)
        {
            startPosition += pivotObject.transform.position;
        }

        transform.position = startPosition - (direction * distance);
        transform.forward = direction.normalized;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotCameraMovement : MonoBehaviour
{
    public GameObject pivotObject = null;
    public bool movementLock = false;

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
    
    public void unlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
    }

	private void Update()
    {
        if(!movementLock)
        {
            if (Input.GetMouseButton(1))
            {
                var rotation = transform.rotation;
                eulerRotation.x -= Input.GetAxis("Mouse Y") * turningRate * Time.deltaTime;
                eulerRotation.x = Mathf.Clamp(eulerRotation.x, 0, 90);
                eulerRotation.y += Input.GetAxis("Mouse X") * turningRate * Time.deltaTime;
                transform.rotation = rotation;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
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
}

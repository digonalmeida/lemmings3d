using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 50;

    [SerializeField]
    private float turningRate = 7000;

    [SerializeField]
    private float zoomSpeed = 1000;

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            var rotation = transform.rotation;
            rotation *= Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * turningRate * Mathf.Deg2Rad * Time.deltaTime, transform.worldToLocalMatrix * transform.right);
            rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turningRate * Mathf.Deg2Rad * Time.deltaTime, transform.worldToLocalMatrix * Vector3.up);
            transform.rotation = rotation;
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 velocity = Vector3.zero;
            velocity += -transform.up * Input.GetAxis("Mouse Y");
            velocity += -transform.right * Input.GetAxis("Mouse X");
            transform.position += velocity * speed * Time.deltaTime;
        }

        transform.position += transform.forward * Time.deltaTime * zoomSpeed * Input.GetAxis("Mouse ScrollWheel");

        var euler = transform.eulerAngles;
        var angle = euler.x;
        if (angle > 180)
        {
            angle -= 360;
        }

        angle = Mathf.Clamp(angle, -60, 60);
        euler.x = angle;
        transform.eulerAngles = euler;
    }
}

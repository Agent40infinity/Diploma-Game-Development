using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Camera attachedCamera;
    public float xSpeed = 120.0f, ySpeed = 120.0f;  // X & Y orbit speed
    public float minYAngle = -20f, maxYAngle = 80f; // minimum & maximum Y limit
    private Vector3 euler;
    private void Start()
    {
        euler = transform.eulerAngles;
    }
    private void LateUpdate()
    {
        euler.x -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
        euler.y += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
        euler.x = Mathf.Clamp(euler.x, minYAngle, maxYAngle);
        transform.eulerAngles = euler;
    }
}
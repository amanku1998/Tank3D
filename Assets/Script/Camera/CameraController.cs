using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 5f, -10f);
    public float smoothSpeed = 5f;
    public float rotationSmoothSpeed = 5f;

    private bool hasTarget = false;

    public float distance = 8f; // Wider view
    public float height = 5f;   // Higher angle

    public float lookDownAngle = 45f; // Angle at which the camera looks down

    //void FixedUpdate()
    //{
    //    if (!hasTarget || target == null) return;

    //    // Desired position behind and above the tank
    //    Vector3 desiredPosition = target.position + offset;

    //    // Smoothly move camera
    //    transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);

    //    // Optionally, look at the tank
    //    Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSmoothSpeed);
    //}

    void FixedUpdate()
    {
        if (!hasTarget || target == null) return;

        // Calculate a backward direction from the tank's forward (rotates with the tank)
        Vector3 backDirection = -target.forward * distance + Vector3.up * height;
        Vector3 desiredPosition = target.position + backDirection;

        // Smoothly move camera
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);

        // Smoothly rotate the camera to look at the tank
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSmoothSpeed);
    }

    // Call this after tank is spawned
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        hasTarget = true;
    }
}

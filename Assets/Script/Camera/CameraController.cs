using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Assigned dynamically after tank spawn
    public Vector3 offset = new Vector3(0f, 5f, -10f);
    public float smoothSpeed = 5f;
    public float rotationSmoothSpeed = 5f;

    private bool hasTarget = false;

    void FixedUpdate()
    {
        if (!hasTarget || target == null) return;

        // Desired position behind and above the tank
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move camera
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);

        // Optionally, look at the tank
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

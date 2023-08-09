using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight_rotation : MonoBehaviour
{
    public float speed = 1f;
    public Camera mainCam;

    void Start()
    {
        // Set the initial rotation of the flashlight to match the camera's orientation
        transform.rotation = mainCam.transform.rotation;
    }

    void Update()
    {
        // Get the position of the mouse in the world space
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCam.nearClipPlane; // Set the z coordinate to the near clip plane of the camera
        Vector3 mouseWorldPosition = mainCam.ScreenToWorldPoint(mousePosition);

        // Calculate the direction between the flashlight's position and the mouse position
        Vector3 direction = mouseWorldPosition - transform.position;

        // Calculate the angle between the direction and the positive x-axis
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Create a quaternion with only the z-axis rotation
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90f);

        // Smoothly rotate the flashlight towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
}
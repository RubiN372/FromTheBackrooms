using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Flashlight_rotation : MonoBehaviour
{
    public float speed = 1f;
    public Camera mainCam;
    

    void Start()
    {
        transform.rotation = mainCam.transform.rotation;
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCam.nearClipPlane; 
        Vector3 mouseWorldPosition = mainCam.ScreenToWorldPoint(mousePosition);

        Vector3 direction = mouseWorldPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90f);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
}
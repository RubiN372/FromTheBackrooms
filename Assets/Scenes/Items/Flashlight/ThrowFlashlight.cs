using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowFlashlight : MonoBehaviour
{
    public FlashlightSwitch flashlightSwitch;
    public Camera mainCam;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Item item = Inventory.instance.FindItemWithName("Flashlight");

            if (item != null && item.GetType() == typeof(Flashlight))
            {
                Flashlight flashlight = (Flashlight)item;

                if (flashlightSwitch.isOn)
                {
                    GameObject throwedFlashlight = flashlight.Throw();
                    Rigidbody2D rigidbody = throwedFlashlight.GetComponent<Rigidbody2D>();

                    Vector3 mousePosition = Input.mousePosition;
                    mousePosition.z = mainCam.nearClipPlane; // Set the z coordinate to the near clip plane of the camera
                    Vector3 mouseWorldPosition = mainCam.ScreenToWorldPoint(mousePosition);

                    // Calculate the direction between the flashlight's position and the mouse position
                    Vector3 direction = mouseWorldPosition - transform.position;
                    
                    rigidbody.AddForce(direction*40);
                    rigidbody.AddTorque(2f);
                }
            }
        }
    }
}

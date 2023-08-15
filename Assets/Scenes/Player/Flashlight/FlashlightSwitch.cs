using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightSwitch : MonoBehaviour
{

    public Light2D flashLight;
    public Light2D flashLightBackLight;
    public AudioClip flashLightOnSound;

    public bool isOn = false;

    private void Awake()
    {
        flashLight.enabled = false;
        isOn = false;
    }

    public void DisableFlashlight()
    {
        isOn = false;
        flashLight.enabled = false;
        flashLightBackLight.enabled = false;
    }

    public void SwitchFlashlight()
    {
        if (Inventory.instance.ContainsItemOfName("Flashlight"))
        {
            isOn = !isOn;
            flashLight.enabled = isOn;
            flashLightBackLight.enabled = isOn;

            if (isOn)
            {
                SoundInstance.InstantiateOnTransform(flashLightOnSound, transform, 0.3f, true);
            }
        }
        else
        {
            isOn = false;
            flashLight.enabled = false;
            flashLightBackLight.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SwitchFlashlight();
        }
    }
}

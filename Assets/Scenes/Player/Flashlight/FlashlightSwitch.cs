using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightSwitch : MonoBehaviour
{
    public bool isOn = false;
    public Light2D flashLight;
    public Light2D flashLightBackLight;
    public AudioClip flashLightOnSound;
    

    private void Awake()
    {
        flashLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            isOn = !isOn;
            flashLight.enabled = isOn;
            flashLightBackLight.enabled = isOn;
            if(isOn)
            {
                SoundInstance.InstantiateOnTransform(flashLightOnSound, transform, 0.3f, true);
            }
        }
    }
}

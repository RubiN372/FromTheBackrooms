using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightSwitch : MonoBehaviour
{
    public AudioClip flashLightOnSound;

    private GameObject holdedFlashlight;
    private GameObject holdedFlashlightInstance;

    public bool isOn = false;

    public void ResetHoldedFlashlight()
    {
        holdedFlashlight = null;
        Destroy(holdedFlashlightInstance);
        isOn = false;
    }

    public void SetHoldedFlashlight(GameObject Holdedflashlight)
    {
        if (holdedFlashlight != null)
        {
            holdedFlashlight.GetComponent<Light2D>().enabled = false;
            isOn = false;
            holdedFlashlight = null;
            Destroy(holdedFlashlightInstance);
        }

        holdedFlashlight = Holdedflashlight;
        holdedFlashlightInstance = Instantiate(holdedFlashlight, transform);
        Debug.Log("Selected flashlight: " + Holdedflashlight);
    }

    private void Awake()
    {
        isOn = false;
        holdedFlashlight = null;
    }

    public void SwitchFlashlight()
    {
        if (holdedFlashlight != null)
        {
            isOn = !isOn;
            holdedFlashlightInstance.GetComponent<Light2D>().enabled = isOn;

            if (isOn)
            {
                SoundInstance.InstantiateOnTransform(flashLightOnSound, transform, 0.3f, true);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SwitchFlashlight();
        }
    }
}

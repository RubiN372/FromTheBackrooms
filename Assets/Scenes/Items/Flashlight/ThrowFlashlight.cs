using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowFlashlight : MonoBehaviour
{   
    [SerializeField] private FlashlightSwitch flashlightSwitch;
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(Inventory.instance.ContainsItemOfName("Flashlight"))
            {
                if(flashlightSwitch.isOn)
                {
                    
                }
            }
        }
    }
}

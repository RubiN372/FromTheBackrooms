using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSSwitch : MonoBehaviour
{
    public GameObject counter;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            counter.SetActive(!counter.activeSelf);
        }
    }
}

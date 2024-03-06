using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightSliderSwitch : MonoBehaviour
{
    [SerializeField] GameObject lightSliderUIprefab;
    [SerializeField] float minCorrectValue;
    [SerializeField] float maxCorrectValue;
    private GameObject sliderUIprefab;
    private bool uiIsActive;

    void OnMouseDown()
    {
        Debug.Log("halo kiurwa");
        sliderUIprefab = Instantiate(lightSliderUIprefab, GameManager.instance.uiCanvas.transform);
        uiIsActive = true;
        PauseUIController.canPause = false;
    }

    void Update()
    {
        if(uiIsActive)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                uiIsActive = false;
                Destroy(sliderUIprefab);
                PauseUIController.canPause = true;
            }
        }
    }

}

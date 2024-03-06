using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LightSliderSwitch : MonoBehaviour
{
    [SerializeField] GameObject lightSliderUIprefab;
    [SerializeField] float correctValueSize;
    [SerializeField] float correctValue;
    [SerializeField] GameObject doorWall;
    [SerializeField] GameObject doorBlockingWall;
    [SerializeField] AudioSource audioSource;

    private GameObject sliderUIprefab;
    private bool uiIsActive;
    private float currentValue;

    void OnMouseDown()
    {
        if(!uiIsActive)
        {
            sliderUIprefab = Instantiate(lightSliderUIprefab, GameManager.instance.uiCanvas.transform);
            uiIsActive = true;
            PauseUIController.canPause = false;
        }
    }

    void Update()
    {
        if(uiIsActive)
        {
            if(sliderUIprefab != null)
                currentValue = sliderUIprefab.GetComponentInChildren<Slider>().value;
            
            if(currentValue >= correctValue - correctValueSize && currentValue <= correctValue + correctValueSize)
                StartCoroutine(CheckValue());
            else
                StopCoroutine(CheckValue());
            
            float proximity = 1f - Mathf.Abs(currentValue - correctValue);
            audioSource.volume = proximity;

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                uiIsActive = false;
                Destroy(sliderUIprefab);
                PauseUIController.canPause = true;
            }
        }
    }

    public IEnumerator CheckValue()
    {
        yield return new WaitForSeconds(1f);

        if(currentValue >= correctValue - correctValueSize && currentValue <= correctValue + correctValueSize)
        {
            doorBlockingWall.SetActive(false);
            doorWall.SetActive(true);
            uiIsActive = false;
            Destroy(sliderUIprefab);

            PauseUIController.canPause = true;
            GetComponent<BoxCollider2D>().enabled = false;
            audioSource.enabled = false;
            enabled = false;
        }
    }

}

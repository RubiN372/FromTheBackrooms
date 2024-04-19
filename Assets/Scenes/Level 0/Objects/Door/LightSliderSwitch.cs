using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class LightSliderSwitch : MonoBehaviour
{
    [SerializeField] GameObject lightSliderUIprefab;
    [SerializeField] float correctValueSize;
    [SerializeField] float correctValue;
    [SerializeField] GameObject doorWall;
    [SerializeField] GameObject doorBlockingWall;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Volume volume;
    [SerializeField] GameObject lightBulb;

    private GameObject sliderUIprefab;
    private bool uiIsActive;
    private float currentValue;
    private float originalGrainIntensity;
    private float originalResponse;

    void Start()
    {
        if (volume.profile.TryGet(out FilmGrain filmGrain))
        {
            originalGrainIntensity = filmGrain.intensity.value;
            originalResponse = filmGrain.response.value;
        }
    }

    void OnMouseDown()
    {
        if (!uiIsActive)
        {
            sliderUIprefab = Instantiate(lightSliderUIprefab, GameManager.instance.uiCanvas.transform);
            uiIsActive = true;
            PauseUIController.canPause = false;
            GameManager.instance.player.GetComponent<PlayerMovement>().enabled = false;
            GameManager.instance.player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            if (volume.profile.TryGet(out FilmGrain filmGrain))
            {
                filmGrain.type.value = FilmGrainLookup.Medium4;
                filmGrain.response.value = 1;
            }
        }
    }

    void Update()
    {
        if (uiIsActive)
        {
            if (sliderUIprefab != null)
                currentValue = sliderUIprefab.GetComponentInChildren<Slider>().value;

            if (currentValue >= correctValue - correctValueSize && currentValue <= correctValue + correctValueSize)
                StartCoroutine(CheckValue());
            else
                StopCoroutine(CheckValue());

            float proximity = 1f - Mathf.Abs(currentValue - correctValue);
            audioSource.volume = proximity;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                uiIsActive = false;
                Destroy(sliderUIprefab);
                PauseUIController.canPause = true;
                GameManager.instance.player.GetComponent<PlayerMovement>().enabled = true;
            }
            if (volume.profile.TryGet(out FilmGrain filmGrain))
            {
                filmGrain.intensity.value = proximity;
            }
        }

    }

    public IEnumerator CheckValue()
    {
        yield return new WaitForSeconds(1.5f);

        if (currentValue >= correctValue - correctValueSize && currentValue <= correctValue + correctValueSize)
        {
            if (volume.profile.TryGet(out FilmGrain filmGrain))
            {
                filmGrain.intensity.value = originalGrainIntensity;
                filmGrain.type.value = FilmGrainLookup.Medium3;
                filmGrain.response.value = originalResponse;
            }
            lightBulb.SetActive(false);
            doorBlockingWall.SetActive(false);
            doorWall.SetActive(true);
            uiIsActive = false;
            Destroy(sliderUIprefab);

            PauseUIController.canPause = true;
            GetComponent<BoxCollider2D>().enabled = false;
            audioSource.enabled = false;
            GameManager.instance.player.GetComponent<PlayerMovement>().enabled = true;
            enabled = false;
        }
    }

}

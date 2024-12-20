using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class AmbienceTrigger : MonoBehaviour
{
    bool wasTriggered = false;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float volumeTransitionTime;
    [SerializeField] float volumeDecreaseStrength;
    GameObject player;
    AudioSource levelAmbience;
    float defaultVolume;
    Coroutine changeVolumeCoroutine;


    void Start()
    {
        player = GameManager.instance.player;
        levelAmbience = player.GetComponentInChildren<AudioSource>();
        defaultVolume = levelAmbience.volume;
    }
    void OnTriggerEnter2D()
    {
        if (changeVolumeCoroutine != null)
            StopCoroutine(changeVolumeCoroutine);
        if(levelAmbience.isActiveAndEnabled)
            changeVolumeCoroutine = StartCoroutine(ChangeVolume(levelAmbience.volume - volumeDecreaseStrength));

        if (!wasTriggered)
        {
            audioSource.Play();
            wasTriggered = true;
        }
    }

    void OnTriggerExit2D()
    {
        if (changeVolumeCoroutine != null)
            StopCoroutine(changeVolumeCoroutine);
        if(levelAmbience.isActiveAndEnabled)
            changeVolumeCoroutine = StartCoroutine(ChangeVolume(defaultVolume));
    }

    IEnumerator ChangeVolume(float targetVolume)
    {
        float timeElapsed = 0f;

        while (timeElapsed < volumeTransitionTime)
        {
            levelAmbience.volume = Mathf.Lerp(levelAmbience.volume, targetVolume, timeElapsed / volumeTransitionTime);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        levelAmbience.volume = targetVolume;
    }
}

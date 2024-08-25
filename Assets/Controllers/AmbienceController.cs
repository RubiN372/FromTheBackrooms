using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambience : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] private float defaultPitch;
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;
    [SerializeField] private float speed;
    [SerializeField] AudioClip[] ambienceList;
    public enum AmbienceList
    {
        Prologue,
        Level_0
    }

    public void SwitchAmbience(AmbienceList ambience, float volume)
    {
        audioSource.volume = volume;
        switch (ambience.ToString())
        {
            case "Prologue":
                audioSource.clip = ambienceList[0];
                audioSource.Play();
                break;
            case "Level_0":
                audioSource.clip = ambienceList[1];
                audioSource.Play();
                break;
        }
    }

    private float t = 0;
    private float newPitch;

    void Start()
    {
        audioSource.pitch = defaultPitch;
        newPitch = Random.Range(minPitch, maxPitch);
    }

    void Update()
    {
        if (t > 1.0f)
        {
            t = 0f;
            newPitch = Random.Range(minPitch, maxPitch);
        }
        else
        {
            audioSource.pitch = Mathf.Lerp(audioSource.pitch, newPitch, t);
            t += speed * Time.deltaTime;
        }
    }
}

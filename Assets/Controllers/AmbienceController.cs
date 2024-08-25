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

    enum AmbienceList
    {
        Level_0


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

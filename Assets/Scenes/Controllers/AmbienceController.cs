using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambience : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [Range(0,2)][SerializeField] private float defaultPitch;
    [Range(0,2)][SerializeField] private float minPitch;
    [Range(0,2)][SerializeField] private float maxPitch; 
    [Range(1,6)][SerializeField] private float speed;

    private float t = 0;
    private float newPitch;
    private bool finishedLerping = true;
    
    void Start()
    {
        audioSource.pitch = defaultPitch;
        newPitch = Random.Range(minPitch, maxPitch);
    }
    
    void Update()
    {
        if(finishedLerping)
        {
            finishedLerping = false;
            audioSource.pitch = Mathf.Lerp(audioSource.pitch, newPitch, t);
            t += speed * Time.deltaTime;   
            if(t > 1.0f)
            {
                t = 0;
                newPitch = Random.Range(minPitch, maxPitch);
            }
        }
    }
}

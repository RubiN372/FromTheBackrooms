using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambience : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float defaultPitch;
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch; 
    [SerializeField] private float speed;


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
        
            if(t > 1.0f)
            {
                t = 0f;  
                newPitch = Random.Range(minPitch, maxPitch);
                finishedLerping = true;
            }
            else
            {
                finishedLerping = false;   
                audioSource.pitch = Mathf.Lerp(audioSource.pitch, newPitch, t);
                t += speed * Time.deltaTime;  
            }
    }
}

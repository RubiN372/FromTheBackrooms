using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class FootstepsManager : MonoBehaviour
{
    [SerializeField] float distanceToMakeSound;
    [SerializeField] AudioClip[] walkingSounds;
    [SerializeField] AudioClip[] sprintingSounds;
    [SerializeField, Range(0.001f, 1f)] float volume;
    [SerializeField] StaminaController staminaController;
    [SerializeField] ParticleSystem dust;

    UnityEngine.Vector3 lastPos;
    float distanceTravelled;

    void CreateDust()
    {
        dust.Play();
    }

    void Start()
    {
        lastPos = transform.position;
        distanceTravelled = 0;
    }
    void Update()
    {
        distanceTravelled += (lastPos - transform.position).magnitude;
        if(distanceTravelled >= distanceToMakeSound)
        {
            if(staminaController.isSprinting)
            {
                SoundInstance.InstantiateOnTransform(sprintingSounds[UnityEngine.Random.Range(0, sprintingSounds.Length)], transform, volume, true, SoundInstance.Randomization.NoRandomization);
                dust.Play();
            }else{
                SoundInstance.InstantiateOnTransform(walkingSounds[UnityEngine.Random.Range(0, walkingSounds.Length)], transform, volume, true, SoundInstance.Randomization.NoRandomization);
            }

            distanceTravelled = 0;
        }
        lastPos = transform.position;
    }
}
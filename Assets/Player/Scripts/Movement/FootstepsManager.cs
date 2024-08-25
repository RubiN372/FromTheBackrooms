using System.Collections;
using UnityEngine;

public class FootstepsManager : MonoBehaviour
{
    [SerializeField] float distanceToMakeSound;
    [SerializeField] AudioClip[] walkingSounds;
    [SerializeField] AudioClip[] sprintingSounds;
    [SerializeField, Range(0.001f, 1f)] float volume;
    [SerializeField] StaminaController staminaController;
    [SerializeField] ParticleSystem dust;

    Vector3 lastPos;
    public float distanceTravelled;
    int frameSkipNumb = 2;
    int frameSkipCounter = 0;

    void Start()
    {
        lastPos = transform.position;
        distanceTravelled = 0;
    }


    void Update()
    {

        if (frameSkipCounter < frameSkipNumb)
        {
            frameSkipCounter++;
            distanceTravelled = 0;
            lastPos = transform.position;
            return;
        }
        distanceTravelled += (lastPos - transform.position).magnitude;

        if (distanceTravelled >= distanceToMakeSound)
        {
            if (staminaController.isSprinting)
            {
                SoundInstance.InstantiateOnTransform(sprintingSounds[Random.Range(0, sprintingSounds.Length)], transform, volume, true, SoundInstance.Randomization.NoRandomization);
                dust.Play();
            }

            else
            {
                SoundInstance.InstantiateOnTransform(walkingSounds[Random.Range(0, walkingSounds.Length)], transform, volume, true, SoundInstance.Randomization.NoRandomization);
            }

            distanceTravelled = 0;
        }

        lastPos = transform.position;

    }
}

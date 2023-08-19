using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlickerEffect : MonoBehaviour
{
    public bool isFlickering = false;
    public float delayBetween;
    public float flickeringDuration;
    public AudioClip flickerSound;
    float timeDelay;
    Light2D light2D;
    private bool CanFlick;
    private bool coroutineIsRunning;

    private void Awake()
    {
        light2D = gameObject.GetComponent<Light2D>();
        CanFlick = false;
        StartCoroutine(FlickDelay());
    }

    void Update()
    {
        if (CanFlick)
        {
            if (isFlickering == false)
            {
                StartCoroutine(FlickeringLight());
            }

        }
        else if (!coroutineIsRunning)
        {
            StartCoroutine(FlickDelay());
        }
    }
    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        float value = Random.Range(0.1f, 0.4f);
        light2D.intensity -= value;

        timeDelay = Random.Range(0.05f, 0.03f);
        yield return new WaitForSeconds(timeDelay);

        light2D.intensity += value;
        timeDelay = Random.Range(0.05f, 0.03f);

        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }

    IEnumerator FlickDelay()
    {
        coroutineIsRunning = true;

        yield return new WaitForSeconds(delayBetween + Random.Range(1, 5));
        SoundInstance.InstantiateOnPos(flickerSound, transform.position, 0.5f, true);
        CanFlick = true;
        yield return new WaitForSeconds(flickeringDuration);

        CanFlick = false;
        coroutineIsRunning = false;
    }
}

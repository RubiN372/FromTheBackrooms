using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenCanvasController : MonoBehaviour
{
    #region Singleton
    public static LoadingScreenCanvasController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion
    [SerializeField] Image image;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Image loadingCircle;
    [SerializeField] GameObject parentObject;
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //bool lerping = false;

    public void FadeOut(float Fadetime)
    {
        StartCoroutine(FadeTransition(Fadetime, false));
    }

    private IEnumerator FadeOutAudio(float fadeDuration)
    {
        if (audioSource == null) yield break;

        float startVolume = audioSource.volume;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
        audioSource.volume = startVolume;
    }


    public IEnumerator FadeTransition(float fadeTime, bool fadeIn)
    {
        //lerping = true;
        float elapsedTime = 0;
        GetComponent<AudioSource>().Play();

        if (fadeIn)
        {
            parentObject.SetActive(true);

            while (elapsedTime < fadeTime)
            {
                float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeTime);
                canvasGroup.alpha = alpha;
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            canvasGroup.alpha = 1;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        }

        else
        {
            while (elapsedTime < fadeTime)
            {
                float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeTime);
                canvasGroup.alpha = alpha;
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            canvasGroup.alpha = 0;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
            parentObject.SetActive(false);
        }

        //lerping = false;
        StartCoroutine(FadeOutAudio(1f));
    }
}

using System.Collections;
using System.Collections.Generic;
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
            parentObject.SetActive(false);
        }
    }
    #endregion
    [SerializeField] Image image;
    [SerializeField] Image loadingCircle;
    [SerializeField] GameObject parentObject;

    bool lerping = false;


    public IEnumerator FadeTransition(float fadeTime, bool fadeIn)
    {
        lerping = true;
        float elapsedTime = 0;
        yield return new WaitForSeconds(1f);

        if (fadeIn)
        {
            Debug.Log("da");
            parentObject.SetActive(true);
            while (elapsedTime < fadeTime)
            {
                image.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, elapsedTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

        }
        else
        {
            while (elapsedTime < fadeTime)
            {
                image.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, elapsedTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            parentObject.SetActive(false);
        }
    }
}

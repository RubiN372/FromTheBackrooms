using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenCanvasController : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Image loadingCircle;
    [SerializeField] GameObject parentObject;

    bool lerping = false;


    void Awake()
    {
        parentObject.SetActive(false);
    }

    void Start()
    {
        StartCoroutine(FadeTransition(4, true));
    }

    IEnumerator FadeTransition(float fadeTime, bool fadeIn)
    {
        lerping = true;
        float elapsedTime = 0;

        if (fadeIn)
        {
            while (elapsedTime < fadeTime)
            {
                image.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, elapsedTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        else
        {

        }



    }

}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class noclip : MonoBehaviour
{
    [SerializeField] float totalTime;
    float currentLerpTime;
    Image image;

    void Awake()
    {
        image = GetComponent<Image>();
        Destroy(gameObject, 3f);
    }

    
    void Update()
    {
        // Increment the currentLerpTime over time
        currentLerpTime += Time.deltaTime;

        // Calculate the interpolation factor based on the currentLerpTime and totalTime
        float t = currentLerpTime / totalTime;

        // Clamp the value to the range [0, 1]
        t = Mathf.Clamp01(t);

        // Set the alpha value using Mathf.Lerp
        image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(0f, 1f, t));

        // Check if the lerping is finished
        if (currentLerpTime >= totalTime)
        {
            // Optionally, you can add some logic here when the lerping is complete
        }
    }
}

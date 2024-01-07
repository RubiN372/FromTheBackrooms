using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PitfallsUIManager : MonoBehaviour
{
    [SerializeField] Image warningSprite;
    [SerializeField] float flashingSpeed;
    [SerializeField] float shownTime;

    private void OnEnable()
    {
        StartCoroutine(WarningFlash());
    }

    private IEnumerator WarningFlash()
    {
        while(true)
        {
            warningSprite.enabled = true;
            yield return new WaitForSeconds(shownTime);
            warningSprite.enabled = false;
            yield return new WaitForSeconds(flashingSpeed);
        }
        
    }
}

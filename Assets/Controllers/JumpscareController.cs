using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class JumpscareController : MonoBehaviour
{
   [SerializeField] GameObject faceUI;
   [SerializeField] GameObject backgroundUI;
   private bool jumpscareRunning = false;
    private IEnumerator PlayJumpscare(float duration, float afterDuration, Vector3 minScale, Vector3 maxScale, RectTransform rectTransform)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            rectTransform.localScale = Vector3.Lerp(minScale, maxScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = maxScale;
        yield return new WaitForSeconds(afterDuration);

        // Reset UI
        rectTransform.localScale = minScale;
        faceUI.SetActive(false);
        backgroundUI.SetActive(false);

        jumpscareRunning = false;
    }

   public void Jumpscare(Sprite face, Vector3 minScale, Vector3 maxScale, float duration, float afterDuration, AudioClip sound)
   {
        if(!jumpscareRunning && face != null && duration > 0 && sound != null)
        {
            jumpscareRunning = true;

            faceUI.SetActive(true);
            backgroundUI.SetActive(true);
            faceUI.GetComponent<Image>().sprite = face;
            RectTransform rectTransform = faceUI.GetComponent<RectTransform>(); 
            
            SoundInstance.InstantiateOnTransform(sound, GameManager.instance.player.transform, 1, true);
            StartCoroutine(PlayJumpscare(duration, afterDuration, minScale, maxScale, rectTransform));
        }
   }
}

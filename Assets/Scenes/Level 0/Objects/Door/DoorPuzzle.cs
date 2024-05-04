using System.Collections;
using UnityEngine;

public class DoorPuzzle : MonoBehaviour
{
    [SerializeField] private float doorsOpeningRange;
    [SerializeField] private AudioClip doorsOpeningSound;
    [SerializeField] private float soundVolume;
    [SerializeField] private float holdingTime;

    private bool isHoldingButton;
    private Coroutine holdingCoroutine;


    void OnMouseDown()
    {
        isHoldingButton = true;
        if (holdingCoroutine == null)
        {
            holdingCoroutine = StartCoroutine(RequiredHoldingTime());
            CursorManager.Instance.SetCursorType(CursorManager.CursorType.LoadingCursor, holdingTime / 28);
        }
        Debug.Log("on");
    }

    void OnMouseExit()
    {
        isHoldingButton = false;
        CursorManager.Instance.SwitchToDefaultCursor();
        if (holdingCoroutine != null)
        {
            StopCoroutine(holdingCoroutine);
            holdingCoroutine = null;
        }
        Debug.Log("off");
    }

    void OnMouseUp()
    {
        isHoldingButton = false;
        CursorManager.Instance.SwitchToDefaultCursor();
        if (holdingCoroutine != null)
        {
            StopCoroutine(holdingCoroutine);
            holdingCoroutine = null;
        }
        Debug.Log("off");
    }

    IEnumerator RequiredHoldingTime()
    {
        yield return new WaitForSeconds(holdingTime);

        if (isHoldingButton)
        {
            Vector2 playerPos = GameManager.instance.player.transform.position;
            float distance = Vector2.Distance(transform.position, playerPos);

            if (distance <= doorsOpeningRange)
            {
                SoundInstance.InstantiateOnPos(doorsOpeningSound, gameObject.transform.position, soundVolume, enabled, SoundInstance.Randomization.NoRandomization);
                CursorManager.Instance.SwitchToDefaultCursor();
                gameObject.SetActive(false);
            }
        }
    }
}

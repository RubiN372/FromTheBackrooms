using UnityEngine;

public class DoorPuzzle : MonoBehaviour
{
    [SerializeField] private float doorsOpeningRange;
    [SerializeField] private AudioClip doorsOpeningSound;
    [SerializeField] private float soundVolume;

    public void OnMouseDown()
    {
        Vector2 playerPos = GameManager.instance.player.transform.position;
        float distance = Vector2.Distance(transform.position, playerPos);

        if (distance <= doorsOpeningRange)
        {
            SoundInstance.InstantiateOnPos(doorsOpeningSound, gameObject.transform.position, soundVolume, enabled, SoundInstance.Randomization.NoRandomization);
            gameObject.SetActive(false);
        }
    }
}

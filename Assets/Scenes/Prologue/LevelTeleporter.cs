using UnityEngine;

public class LevelTeleporter : MonoBehaviour
{
    bool pressed = false;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (!pressed)
        {
            if (coll.collider.gameObject.CompareTag("Player"))
            {
                Loader.Instance.LoadScene(Loader.Scene.Level_0, Loader.Scene.Prologue, new Vector2(-139.94f, -76.81f), Ambience.AmbienceList.Level_0, 0.1f);
                pressed = true;
            }
        }


    }
}

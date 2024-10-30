using UnityEngine;

public class LevelTeleporter : MonoBehaviour
{
    GameObject player;
    bool pressed = false;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (!pressed)
        {
            if (coll.collider.gameObject.CompareTag("Player"))
            {
                Loader.Instance.LoadScene(Loader.Scene.Level_0, Loader.Scene.Prologue, new Vector2(-136.82f, -76.32f), Ambience.AmbienceList.Level_0, 0.1f);
                pressed = true;
            }
        }


    }
}

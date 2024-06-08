using UnityEngine;

public class LevelTeleporter : MonoBehaviour
{
    GameObject player;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.gameObject.CompareTag("Player"))
        {
            Loader.UnloadSceneAsync(Loader.Scene.Prologue);
            Loader.LoadAsync(Loader.Scene.Level_0, false);
        }
    }
}

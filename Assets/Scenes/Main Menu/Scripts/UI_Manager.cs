using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] GameObject loadingCircle;
    public void PlayButton()
    {
        loadingCircle.SetActive(true);
        Loader.LoadAsync(Loader.Scene.Prologue, true);
        Loader.LoadAsync(Loader.Scene.PlayerScene, false);
    }
}

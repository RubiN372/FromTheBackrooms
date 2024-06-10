using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] GameObject loadingCircle;
    public void PlayButton()
    {
        loadingCircle.SetActive(true);
        Loader.LoadAsync(Loader.Scene.PlayerScene, true);
        Loader.LoadAsync(Loader.Scene.Level_0, false);

    }
}

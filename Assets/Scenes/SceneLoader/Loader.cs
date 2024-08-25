using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    #region Singleton
    public static Loader Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    public enum Scene
    {
        Main_Menu,
        PlayerScene,
        Prologue,
        Loading_Screen,
        Level_0
    }
    [SerializeField] Slider progressSlider;

    public IEnumerator UnloadSceneAsync(Scene scene)
    {
        UnityEngine.AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(scene.ToString());

        while (!asyncUnload.isDone)
        {
            yield return null;
        }
    }

    public void LoadFromMenu()
    {
        StartCoroutine(LoadInFromMainMenu());
    }

    public void LoadScene(Scene scene, Scene sceneToUnload, Vector2 playerTeleportPos, Ambience.AmbienceList ambienceType, float ambienceVolume)
    {
        StartCoroutine(LoadAsync(scene, sceneToUnload, playerTeleportPos, ambienceType, ambienceVolume));
    }

    private IEnumerator LoadInFromMainMenu()
    {
        UnityEngine.AsyncOperation asyncLoad;
        UnityEngine.AsyncOperation asyncLoad2;
        yield return StartCoroutine(LoadingScreenCanvasController.Instance.FadeTransition(1f, true));
        asyncLoad = SceneManager.LoadSceneAsync("PlayerScene", LoadSceneMode.Single);

        float progress;
        while (!asyncLoad.isDone)
        {
            progress = asyncLoad.progress / 2;
            progressSlider.value = progress;
            yield return null;
        }
        GameManager.instance.ambience.SwitchAmbience(Ambience.AmbienceList.Prologue, 0.025f);
        GameManager.instance.player.transform.position = new Vector2(0, 0);
        GameManager.instance.cinemachine.PreviousStateIsValid = false;

        asyncLoad2 = SceneManager.LoadSceneAsync("Prologue", LoadSceneMode.Additive);
        while (!asyncLoad2.isDone)
        {
            progress = 0.5f + asyncLoad2.progress / 2;
            progressSlider.value = progress;
            yield return null;
        }
        LoadingScreenCanvasController.Instance.FadeOut(1f);
    }

    private IEnumerator LoadAsync(Scene sceneToLoad, Scene sceneToUnload, Vector2 playerTeleportPos, Ambience.AmbienceList ambienceType, float ambienceVolume)
    {
        float progress = 0f;
        AudioSource ambience = GameManager.instance.ambience.audioSource;
        UnityEngine.AsyncOperation asyncUnload;
        UnityEngine.AsyncOperation asyncLoad2;

        yield return StartCoroutine(LoadingScreenCanvasController.Instance.FadeTransition(1f, true));
        ambience.Stop();
        asyncUnload = SceneManager.UnloadSceneAsync(sceneToUnload.ToString());

        while (!asyncUnload.isDone)
        {
            progress = asyncUnload.progress / 2;
            progressSlider.value = progress;
            yield return null;
        }

        progress = asyncUnload.progress / 2;
        progressSlider.value = progress;

        GameManager.instance.player.GetComponent<AudioListener>().enabled = false;
        GetComponent<AudioListener>().enabled = true;
        GameManager.instance.player.GetComponent<PlayerMovement>().enabled = false;
        GameManager.instance.player.transform.position = playerTeleportPos;
        GameManager.instance.cinemachine.PreviousStateIsValid = false;

        asyncLoad2 = SceneManager.LoadSceneAsync(sceneToLoad.ToString(), LoadSceneMode.Additive);
        while (!asyncLoad2.isDone)
        {
            progress = 0.5f + asyncLoad2.progress / 2;
            progressSlider.value = progress;
            yield return null;
        }

        progress = 0.5f + asyncLoad2.progress / 2;
        progressSlider.value = progress;

        GameManager.instance.ambience.SwitchAmbience(ambienceType, ambienceVolume);
        GetComponent<AudioListener>().enabled = false;
        GameManager.instance.player.GetComponent<AudioListener>().enabled = true;
        GameManager.instance.player.GetComponent<PlayerMovement>().enabled = true;
        LoadingScreenCanvasController.Instance.FadeOut(1f);
    }

}

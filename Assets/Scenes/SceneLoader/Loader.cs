using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void LoadScene(Scene scene, Scene sceneToUnload)
    {
        StartCoroutine(LoadAsync(scene, sceneToUnload));
    }

    private IEnumerator LoadInFromMainMenu()
    {
        UnityEngine.AsyncOperation asyncLoad;
        UnityEngine.AsyncOperation asyncLoad2;

        yield return StartCoroutine(LoadingScreenCanvasController.Instance.FadeTransition(1f, true));
        asyncLoad = SceneManager.LoadSceneAsync("PlayerScene", LoadSceneMode.Single);

        while (!asyncLoad.isDone)
        {
            yield return null;
            Debug.Log(asyncLoad.progress);
        }

        asyncLoad2 = SceneManager.LoadSceneAsync("Prologue", LoadSceneMode.Additive);
        while (!asyncLoad2.isDone)
        {
            yield return null;
        }
        LoadingScreenCanvasController.Instance.FadeOut(1f);
    }

    private IEnumerator LoadAsync(Scene sceneToLoad, Scene sceneToUnload)
    {
        UnityEngine.AsyncOperation asyncUnload;
        UnityEngine.AsyncOperation asyncLoad2;

        yield return StartCoroutine(LoadingScreenCanvasController.Instance.FadeTransition(1f, true));
        asyncUnload = SceneManager.UnloadSceneAsync(sceneToUnload.ToString());

        while (!asyncUnload.isDone)
        {
            yield return null;
            Debug.Log(asyncUnload.progress);
        }

        asyncLoad2 = SceneManager.LoadSceneAsync(sceneToLoad.ToString(), LoadSceneMode.Additive);
        while (!asyncLoad2.isDone)
        {
            yield return null;
        }
        LoadingScreenCanvasController.Instance.FadeOut(1f);
    }

}

using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        Main_Menu,
        PlayerScene,
        Prologue,
        Level_0
    }

    public static void UnloadSceneAsync(Scene scene)
    {
        SceneManager.UnloadSceneAsync(scene.ToString());
    }

    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public static void LoadAsync(Scene scene, bool single)
    {
        if (single)
            SceneManager.LoadSceneAsync(scene.ToString());
        else
            SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive);
    }
}

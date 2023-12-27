using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        Main_Menu,
        Level_0
    }

    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public static void LoadAsync(Scene scene, bool single)
    {
        if(single)
            SceneManager.LoadSceneAsync(scene.ToString());
        else 
            SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive);
    }
}

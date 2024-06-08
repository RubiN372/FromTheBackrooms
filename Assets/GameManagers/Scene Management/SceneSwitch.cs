#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;


public static class SceneSwitch
{
    private static string PreviousScene;

    [MenuItem("Scene Switch/MainMenu")]
    static void ChangeSceneToMainMenu()
    {
        for (int i = 0; i < EditorSceneManager.sceneCount; i++)
        {
            if (EditorSceneManager.GetSceneAt(i).isDirty)
            {
                Debug.Log("One of the scenes needs a saverino!");
                return;
            }
        }
        PreviousScene = EditorSceneManager.GetActiveScene().path;

        EditorSceneManager.OpenScene("Assets/Scenes/Main Menu/Main_Menu.unity");
    }

    [MenuItem("Scene Switch/Prologue")]
    static void ChangeSceneToPrologue()
    {
        for (int i = 0; i < EditorSceneManager.sceneCount; i++)
        {
            if (EditorSceneManager.GetSceneAt(i).isDirty)
            {
                Debug.Log("One of the scenes needs a saverino!");
                return;
            }
        }
        PreviousScene = EditorSceneManager.GetActiveScene().path;

        EditorSceneManager.OpenScene("Assets/Scenes/Prologue/Prologue.unity");
        EditorSceneManager.OpenScene("Assets/GameManagers/PlayerScene.unity", OpenSceneMode.Additive);
    }

    [MenuItem("Scene Switch/Level 0")]
    static void ChangeSceneToLevel0()
    {
        for (int i = 0; i < EditorSceneManager.sceneCount; i++)
        {
            if (EditorSceneManager.GetSceneAt(i).isDirty)
            {
                Debug.Log("One of the scenes needs a saverino!");
                return;
            }
        }
        PreviousScene = EditorSceneManager.GetActiveScene().path;

        EditorSceneManager.OpenScene("Assets/Scenes/Level 0/Level_0.unity");
        EditorSceneManager.OpenScene("Assets/GameManagers/PlayerScene.unity", OpenSceneMode.Additive);
    }
}

#endif
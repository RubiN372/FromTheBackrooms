using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPlayerScene : MonoBehaviour
{
    void Awake()
    {
        if (SceneManager.GetSceneByName("PlayerScene") == null)
            SceneManager.LoadScene("PlayerScene", LoadSceneMode.Additive);
    }
}

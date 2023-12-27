using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class PlayerController : MonoBehaviour
{
    private void Awake()
    {
        GameManager.instance.AddPlayer(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}

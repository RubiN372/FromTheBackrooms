using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class PlayerController : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.AddPlayer(gameObject);
    }
}

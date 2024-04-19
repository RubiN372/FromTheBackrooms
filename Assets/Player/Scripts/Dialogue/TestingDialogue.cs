using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestingDialogue : MonoBehaviour
{
    [SerializeField] DialogueController dialogueController;
    public void OnTriggerEnter2D()
    {
        dialogueController.AddDialogue("What is this place?", Color.white, 0.08f, 2f);
        gameObject.SetActive(false);
    }
}

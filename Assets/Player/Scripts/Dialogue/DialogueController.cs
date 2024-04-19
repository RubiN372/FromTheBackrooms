using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

struct Dialogue
{
    public string dialogueMessage;
    public Color defaultColor;
    public float typingSpeed;
    public float lastingDelay;

    public Dialogue(string message, Color color, float typeSpeed, float delay)
    {
        dialogueMessage = message;
        defaultColor = color;
        typingSpeed = typeSpeed;
        lastingDelay = delay;
    }
}

public class DialogueController : MonoBehaviour
{
    List<Dialogue> dialogueQueue = new List<Dialogue>();
    [SerializeField] TextMeshProUGUI textComponent;

    public void AddDialogue(string message, Color color, float typeSpeed, float lastingDelay)
    {
        Dialogue dialogue = new Dialogue(message, color, typeSpeed, lastingDelay);
        dialogueQueue.Add(dialogue);
        StartCoroutine(Type(dialogue.typingSpeed));
    }

    IEnumerator Type(float timeSeconds)
    {
        string textToType = dialogueQueue[0].dialogueMessage;
        textComponent.color = dialogueQueue[0].defaultColor;

        for (int i = 0; i < textToType.Length; i++)
        {
            textComponent.SetText(textComponent.text + textToType[i].ToString());
            yield return new WaitForSeconds(timeSeconds);
        }

        yield return new WaitForSeconds(dialogueQueue[0].lastingDelay);
        dialogueQueue.Remove(dialogueQueue[0]);
        textComponent.SetText("");

    }

}


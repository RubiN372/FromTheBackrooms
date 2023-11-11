using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSwitchController : MonoBehaviour, IInteractable
{
    [SerializeField] Light2D light2d;
    [SerializeField] Sprite onTexture;
    [SerializeField] Sprite offTexture;
    [SerializeField] SpriteRenderer sRenderer;
    [SerializeField] AudioClip switchOn;
    [SerializeField] AudioClip switchOff;
    [SerializeField] GlitchedWall glitchedWall;
    public bool isOn{ get; private set; }

    void Start()
    {
        light2d.enabled = isOn;
        if(isOn)
            sRenderer.sprite = onTexture;
        else
            sRenderer.sprite = offTexture;
        
    }
    public void Switch(bool IsOn)
    {
        isOn = IsOn;
        light2d.enabled = isOn;
        if(isOn)
        {
            sRenderer.sprite = onTexture;
            SoundInstance.InstantiateOnTransform(switchOn, transform, 0.35f, true, SoundInstance.Randomization.NoRandomization);
        }
            
        else
        {
            sRenderer.sprite = offTexture;
            SoundInstance.InstantiateOnTransform(switchOff, transform, 0.35f, true, SoundInstance.Randomization.NoRandomization);
        }    
    }

    void OnMouseDown()
    {
        GameManager.instance.player.GetComponent<ItemInteract>().InteractWithObject(gameObject ,transform.position); 
    }

    public void Interact()
    {
        Switch(!isOn);
        glitchedWall.CheckForState();
    }
}

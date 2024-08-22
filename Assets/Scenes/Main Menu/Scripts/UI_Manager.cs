using System.Collections;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    bool pressed = false;
    public void PlayButton()
    {
        if (!pressed)
        {
            pressed = true;
            Loader.Instance.LoadFromMenu();
        }
    }
}



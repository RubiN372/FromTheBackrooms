using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHoverEffect : MonoBehaviour
{
    void OnMouseEnter()
    {
        CursorManager.Instance.SwitchToDefaultHoverCursor();
        Debug.Log("1");
    }

    void OnMouseExit()
    {
        CursorManager.Instance.SwitchToDefaultCursor();
        Debug.Log("2");
    }

}

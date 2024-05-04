using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    void Start()
    {
        //StartCoroutine(testingc());
    }

    IEnumerator testingc()
    {
        yield return new WaitForSeconds(3);
        CursorManager.Instance.SwitchToDefaultCursor();
    }
}

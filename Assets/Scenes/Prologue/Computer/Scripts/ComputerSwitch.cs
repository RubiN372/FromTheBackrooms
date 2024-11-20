using Unity.VisualScripting;
using UnityEngine;

public class ComputerSwitch : MonoBehaviour
{
    [SerializeField]
    Computer computerScript;

    void OnMouseDown()
    {
        computerScript.LaunchPC();
    }
}

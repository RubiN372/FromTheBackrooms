using UnityEngine;
using UnityEngine.UI;
public class Computer : MonoBehaviour
{
    [SerializeField] GameObject monitor;
    bool isOn = false;

    public void LaunchPC()
    {
        Debug.Log("test1");
        if (!isOn)
        {
            Debug.Log("test2");
            isOn = true;
            monitor.GetComponent<SpriteRenderer>().color = new Color(0, 50, 255);
        }
    }


}

using Unity.VisualScripting;
using UnityEngine;

public class GlitchedWall : MonoBehaviour
{
    bool isActivated = false;
    public LightSwitchController lightSwitch1;
    public LightSwitchController lightSwitch2;
    public LightSwitchController lightSwitch3;

    // Checks for light switches state if they are on or off
    public void CheckForState()
    {
        if(!lightSwitch1.isOn && lightSwitch2.isOn && lightSwitch3.isOn)
        {
            isActivated = true;
            Debug.Log("true");
        }else{
            isActivated = false;
            Debug.Log("false");
        }
    }
    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(isActivated && collision.collider.CompareTag("Player"))
        {
            //teleport
        }   
    }
}

using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Flashlight")]
public class Flashlight : Item
{
    public GameObject ThrowedFlashlight;
    public GameObject FlashlightLight;
    public override GameObject Drop()
    {
        GameManager.instance.player.GetComponentInChildren<FlashlightSwitch>().ResetHoldedFlashlight();

        return Instantiate(itemPrefab, GameManager.instance.player.transform.position, itemPrefab.transform.rotation);
    }
    
    public GameObject Throw()
    {
        GameManager.instance.player.GetComponentInChildren<FlashlightSwitch>().ResetHoldedFlashlight();
        Vector2 pos = GameManager.instance.player.transform.position;
        Inventory.instance.Remove(this);
        return Instantiate(ThrowedFlashlight, GameManager.instance.player.transform.position, ThrowedFlashlight.transform.rotation);
    }
    public override void Use()
    {
        GameManager.instance.player.GetComponentInChildren<FlashlightSwitch>().SetHoldedFlashlight(FlashlightLight);
    }


}

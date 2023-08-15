using JetBrains.Annotations;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Flashlight")]
public class Flashlight : Item
{
    public GameObject ThrowedFlashlight;
    public override GameObject Drop()
    {
        Vector2 pos = GameManager.instance.player.transform.position;
        return Instantiate(itemPrefab, GameManager.instance.player.transform.position, itemPrefab.transform.rotation);
    }
    
    public GameObject Throw()
    {
        Vector2 pos = GameManager.instance.player.transform.position;
        Inventory.instance.Remove(this);
        return Instantiate(ThrowedFlashlight, GameManager.instance.player.transform.position, ThrowedFlashlight.transform.rotation);
    }
}

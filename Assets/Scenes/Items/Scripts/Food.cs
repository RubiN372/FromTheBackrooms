using UnityEngine;
using DeepWolf.HungerThirstSystem;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Food")]
public class Food : Item
{
    public float hungerValue = 150f;

    public override void Use()
    {
        HungerThirst hungerThirst = GameManager.instance.player.GetComponent<HungerThirst>();
        if (hungerThirst != null && hungerThirst.UseThirst)
        {
            hungerThirst.ReduceHunger(hungerValue);
            Inventory.instance.Remove(this);
        }
    }
}

using UnityEngine;
using DeepWolf.HungerThirstSystem;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Drink")]
public class Drink : Item
{
    public float drinkValue = 150f;

    public override void Use()
    {
        
        HungerThirst hungerThirst = GameManager.instance.player.GetComponent<HungerThirst>();
        if (hungerThirst != null && hungerThirst.UseThirst)
        {
            hungerThirst.ReduceThirst(drinkValue);
            Inventory.instance.Remove(this);
        }
    }
}

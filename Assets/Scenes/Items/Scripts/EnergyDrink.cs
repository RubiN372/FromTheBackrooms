using UnityEngine;
using DeepWolf.HungerThirstSystem;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/EnergyDrink")]
public class EnergyDrink : Drink
{

    public float staminaValue;

    public override void Use()
    {
        HungerThirst hungerThirst = GameManager.instance.player.GetComponent<HungerThirst>();
        StaminaController staminaController = GameManager.instance.player.GetComponent<StaminaController>();
        if (hungerThirst != null && hungerThirst.UseThirst)
        {
            hungerThirst.ReduceThirst(drinkValue);
            staminaController.AddStamina(staminaValue);
            Inventory.instance.Remove(this);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
// To use any of the Hunger Thirst System, you have to add this using.
using DeepWolf.HungerThirstSystem;
public class HungerThirstUI : MonoBehaviour
{
    // The reference to the hunger & thirst component. Assign it in the inspector
    [SerializeField] private HungerThirst hungerThirst;
    // The UI stuff
    [Header("[UI]")]
    [SerializeField] private Image hungerBar;
    [SerializeField] private Image thirstBar;
    // Refresh hunger and thirst when the script object is initialized
    private void Awake()
    {
        RefreshHunger();
        RefreshThirst();
    }
    // You can let the [On Thirst Changed] event call this method.
    public void RefreshThirst()
    {
        // Set the thirst label text to the value of the current thirst
        thirstBar.fillAmount = 1 - (hungerThirst.Thirst / 1000);
    }
    // You can let the [On Hunger Changed] event call this method.
    public void RefreshHunger()
    {
        // Set the hunger label text to the value of the current hunger
        hungerBar.fillAmount = 1 - (hungerThirst.Hunger / 1000);
    }
}

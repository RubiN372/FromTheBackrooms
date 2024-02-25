using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    [Header("Sprint Settings")]
    [SerializeField] private float maxStamina = 100;
    public float playerStamina;
    public float sprintMultiplier = 3f;
    [Range(0, 50)][SerializeField] private float staminaDrain = 0.5f;
    [Range(0, 50)][SerializeField] private float staminaRegen = 0.5f;
    public bool hasRegenerated = true;
    public bool isSprinting;
    [SerializeField] private Image bar;
    [SerializeField] private PlayerMovement playerMovement;

    void Update()
    {
        if (!isSprinting && playerStamina <= maxStamina - 0.01f)
        {
            playerStamina += staminaRegen * Time.deltaTime;
            UpdateStamina(1);

            if (playerStamina >= maxStamina - 0.01f)
            {
                hasRegenerated = true;
                playerStamina = maxStamina;
            }
        }
    }

    public void AddStamina(float value)
    {
        if (playerStamina + value > maxStamina)
        {
            playerStamina = maxStamina;
        }
        else
        {
            playerStamina += value;
        }

        UpdateStamina(1);
    }

    void UpdateStamina(int value)
    {
        bar.fillAmount = playerStamina / maxStamina;
    }

    public void Sprinting()
    {
        if (hasRegenerated)
        {
            isSprinting = true;
            playerStamina -= staminaDrain * Time.deltaTime;
            UpdateStamina(1);

            if (playerStamina <= 0)
            {
                hasRegenerated = false;
            }
        }
    }
}

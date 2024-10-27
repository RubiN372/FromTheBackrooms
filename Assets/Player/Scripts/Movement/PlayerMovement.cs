using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] StaminaController staminaController;
    Rigidbody2D rb;
    Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && staminaController.playerStamina > 0 && staminaController.hasRegenerated)
        {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed * staminaController.sprintMultiplier;

            if (movement.sqrMagnitude > 0)
            {
                staminaController.Sprinting();
                animator.SetBool("Sprinting", true);
            }

            else
            {
                staminaController.isSprinting = false;
                animator.SetBool("Sprinting", false);
            }

        }

        else
        {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;

            staminaController.isSprinting = false;
            animator.SetBool("Sprinting", false);
        }

        rb.linearVelocity = movement;

        animator.SetFloat("Speed", movement.sqrMagnitude);

        animator.SetFloat("Velocity X", rb.linearVelocity.x);
        animator.SetFloat("Velocity Y", rb.linearVelocity.y);

    }
}

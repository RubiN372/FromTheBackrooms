using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmilerAnimationController : MonoBehaviour
{   public Transform target; // The object your character should look at
    [SerializeField] private Sprite[] sprites; // An array of sprites for different directions
    [SerializeField] private SmilerAI smilerAI;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Update()
    {
        if (smilerAI.isChasing && target != null)
        {
            // Calculate the direction from your character to the target object
            Vector3 direction = (target.position - transform.position).normalized;

            // Calculate the angle of the direction vector
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Determine which sprite to use based on the angle
            int spriteIndex = Mathf.RoundToInt((angle + 180f) / 45f) % 8;

            // Update the sprite renderer with the appropriate sprite
            if (spriteIndex >= 0 && spriteIndex < sprites.Length)
            {
                spriteRenderer.sprite = sprites[spriteIndex];
            }
        }else{
            spriteRenderer.sprite = sprites[8];
        }
    }
}

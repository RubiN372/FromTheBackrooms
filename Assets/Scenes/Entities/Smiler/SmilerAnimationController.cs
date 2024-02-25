using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmilerAnimationController : MonoBehaviour
{
    public Transform target;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private SmilerAI smilerAI;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Update()
    {
        if (smilerAI.isChasing && target != null)
        {
            // Calculate the angle of the direction vector
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            int spriteIndex = Mathf.RoundToInt((angle + 180f) / 45f) % 8;

            if (spriteIndex >= 0 && spriteIndex < sprites.Length)
            {
                spriteRenderer.sprite = sprites[spriteIndex];
            }
        }
        else
        {
            spriteRenderer.sprite = sprites[8];
        }
    }
}

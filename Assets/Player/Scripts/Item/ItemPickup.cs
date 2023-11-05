using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private float pickupRadius = 0.85f;
    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private LayerMask obstacleLayer; // Layer containing obstacles

    private Item item;

    public bool PickupItem(Item item, Vector2 pos)
    {
        float distance = Vector2.Distance(transform.position, pos);

        if (distance < pickupRadius)
        {
            Vector2 direction = pos - (Vector2)transform.position;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, distance, itemLayer);

            if (hit.collider != null)
            {
                // Ray hit an object on the "item" layer.
                // Now perform a line of sight check to ensure no obstacles in between.
                Vector2 rayDirection = pos - (Vector2)transform.position;
                RaycastHit2D lineOfSightHit = Physics2D.Raycast(transform.position, rayDirection.normalized, distance, obstacleLayer);

                if (lineOfSightHit.collider == null)
                {
                    if (Inventory.instance.Add(item))
                    {
                        Debug.Log("Picking up " + item.name);
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
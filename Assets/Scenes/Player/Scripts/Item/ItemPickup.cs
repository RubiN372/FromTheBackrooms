using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private float pickupRadius = 0.85f;
    private Item item;

    public bool PickupItem(Item item, Vector2 pos)
    {
            float distance = Vector2.Distance(transform.position, pos);

            if(distance < pickupRadius)
            {
                if(Inventory.instance.Add(item))
                {
                    Debug.Log("Picking up " + item.name);
                    return true;
                }
            }             
            return false;
    }
 
       
    
}

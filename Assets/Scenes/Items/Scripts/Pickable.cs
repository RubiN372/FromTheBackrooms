using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public bool isPickable = true;
    public Item item;


    private void OnMouseDown() { 

        if(GameManager.instance.player.GetComponent<ItemPickup>().PickupItem(item, transform.position))
        {
            Destroy(gameObject);
        }
        
    }
}

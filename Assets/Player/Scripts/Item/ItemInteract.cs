using UnityEngine;

public class ItemInteract : MonoBehaviour
{
    [SerializeField] private float interactRadius = 0.85f;
    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private LayerMask obstacleLayer; 

    public bool PickupItem(Item item, Vector2 pos)
    {
        float distance = Vector2.Distance(transform.position, pos);

        if (distance < interactRadius)
        {
            Vector2 direction = pos - (Vector2)transform.position;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, distance, itemLayer);

            if (hit.collider != null)
            {
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

    public bool InteractWithObject(GameObject gm, Vector2 pos)
    {
        float distance = Vector2.Distance(transform.position, pos);

        if (distance < interactRadius)
        {
            Debug.Log("halo1");
            Vector2 direction = pos - (Vector2)transform.position;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, distance, interactableLayer);

            if (hit.collider != null)
            {
                Debug.Log("halo2");

                Debug.Log("halo3");
                IInteractable interactable = gm.GetComponent<IInteractable>();
                if(interactable == null)
                    return false;

                interactable.Interact();    
                return true;
            }
        }
        return false;
    }
}
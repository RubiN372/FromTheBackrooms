using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    Item item;
    public Image icon;
    public Button removeButton;
    public Button useButton;
    public GameObject player;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
        useButton.interactable = true;
        icon.rectTransform.sizeDelta = new Vector2(item.iconWidth, item.iconHeight);
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        useButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        item.Drop();
        Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}

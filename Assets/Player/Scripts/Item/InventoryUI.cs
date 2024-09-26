using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    Inventory inventory;
    InventorySlot[] slots;
    public GameObject inventoryUI;
    public bool isOpen = false;

    private void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    public void ChangeUIVisibility(bool isShown)
    {
        inventoryUI.SetActive(isShown);
        isOpen = isShown;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeUIVisibility(!isOpen);
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}

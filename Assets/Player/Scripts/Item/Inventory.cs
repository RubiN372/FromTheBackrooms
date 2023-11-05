using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found!");
            return;
        }
        instance = this;
    }
    #endregion
    public UnityEvent onItemChangedTrigger;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public List<Item> items = new List<Item>();
    public int space = 12;

    public bool Add(Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("Not enough room to add the item!");
            return false;
        }

        items.Add(item);
        if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
        if (onItemChangedTrigger != null) onItemChangedTrigger.Invoke();


        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
        if (onItemChangedTrigger != null) onItemChangedTrigger.Invoke();
    }

    public bool ContainsItemOfName(string name)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].name == name)
            {
                return true;
            }
        }
        return false;
    }

    public Item FindItemWithName(string name)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].name == name)
            {
                return items[i];
            }
        }
        return null;
    }
}

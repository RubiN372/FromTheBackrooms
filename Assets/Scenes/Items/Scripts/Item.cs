using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public GameObject itemPrefab;
    public float iconWidth, iconHeight;

    public virtual void Use()
    {
       Debug.Log("Using " + name);
    }

    public GameObject Drop()
    {
        Vector2 pos = GameManager.instance.player.transform.position;
        return Instantiate(itemPrefab, GameManager.instance.player.transform.position, itemPrefab.transform.rotation);
    }
}

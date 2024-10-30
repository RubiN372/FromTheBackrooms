using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/MEGDocument")]
public class MEGDocument : Item
{
    public Sprite paperUISprite;
    public int sixeX, sizeY;

    public override void Use()
    {
        base.Use();
        GameManager.instance.GetViewedObject().ViewObject(paperUISprite, sixeX, sizeY);
        Debug.Log("tutaj dziala");
    }

    public override GameObject Drop()
    {
        GameManager.instance.GetViewedObject().CloseObject();
        return base.Drop();
    }
}

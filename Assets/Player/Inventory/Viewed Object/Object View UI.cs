using UnityEngine.UI;
using UnityEngine;

public class ObjectViewUI : MonoBehaviour
{
    bool isObjectViewed;
    [SerializeField] GameObject objectImage;
    [SerializeField] GameObject objectHolder;

    void Awake()
    {
        objectHolder.SetActive(false);
    }

    public void ViewObject(Sprite image, int sizeX, int sizeY)
    {
        if(image == null)
            return;
        if(isObjectViewed)
        {
            
            CloseObject();
            return;
        }
            
        isObjectViewed = true;
        objectHolder.SetActive(true);
        objectImage.GetComponent<UnityEngine.UI.Image>().sprite = image;
        objectImage.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX, sizeY);
    }

    public void CloseObject()
    {
        if(!isObjectViewed)
            return;
        isObjectViewed = false;
        objectHolder.SetActive(false);
    }

}

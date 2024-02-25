using UnityEngine;

public class WarningController : MonoBehaviour
{
    [SerializeField] GameObject warningUIprefab;
    private GameObject warningUI;
    void OnTriggerEnter2D()
    {
        warningUI = Instantiate(warningUIprefab, GameManager.instance.uiCanvas.transform);
    }

    void OnTriggerExit2D()
    {
        if (warningUI == null)
            return;

        Destroy(warningUI);
    }
}

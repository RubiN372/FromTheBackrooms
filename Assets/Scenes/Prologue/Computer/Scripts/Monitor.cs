using UnityEngine;

public class Monitor : MonoBehaviour
{
    [SerializeField] GameObject computerUIPrefab;
    private GameObject computerUIInstance;
    [SerializeField] Computer computer;
    void OnMouseDown()
    {
        if (computer.isOn)
        {
            Debug.Log(GameManager.instance.uiCanvas.transform);
            computerUIInstance = Instantiate(computerUIPrefab, GameManager.instance.uiCanvas.transform);
            GameObject player = GameManager.instance.player;
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        }
    }
}

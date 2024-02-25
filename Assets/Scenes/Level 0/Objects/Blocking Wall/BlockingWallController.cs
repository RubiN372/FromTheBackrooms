using UnityEngine;

public class BlockingWallController : MonoBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] GameObject fakeWall;
    public void OnTriggerEnter2D()
    {
        fakeWall.SetActive(false);
        wall.SetActive(true);
    }
}

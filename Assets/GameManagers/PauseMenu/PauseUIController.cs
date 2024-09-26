using UnityEngine;

public class PauseUIController : MonoBehaviour
{
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject ambience;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] InventoryUI inventoryUI;
    public static bool isPaused { get; private set; } = false;
    public static bool canPause = true;
    private bool wasInventoryOpen = false;

    void Start()
    {
        pauseUI.SetActive(false);
    }

    private void SetActiveComponents(bool enabled)
    {
        ambience.SetActive(enabled);
        playerMovement.enabled = enabled;
    }


    public void Resume()
    {
        isPaused = false;
        pauseUI.SetActive(false);
        SetActiveComponents(true);
        inventoryUI.ChangeUIVisibility(wasInventoryOpen);
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        if (!canPause)
            return;
        isPaused = true;
        pauseUI.SetActive(true);
        SetActiveComponents(false);
        Time.timeScale = 0f;
        wasInventoryOpen = inventoryUI.isOpen;
        inventoryUI.ChangeUIVisibility(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == false)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }
}

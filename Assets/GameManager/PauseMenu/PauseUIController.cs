using UnityEngine;

public class PauseUIController : MonoBehaviour
{
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject ambience;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] InventoryUI inventoryUI;
    public static bool isPaused {get; private set;} = false;
    public static bool canPause = true;

    void Start()
    {
        pauseUI.SetActive(false);
    }

    private void SetActiveComponents(bool enabled)
    {
        ambience.SetActive(enabled);
        playerMovement.enabled = this.enabled;
        inventoryUI.enabled = this.enabled;
    }


    public void Resume()
    {
         isPaused = false;
        pauseUI.SetActive(false);
        SetActiveComponents(true);
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        if(!canPause)
            return;
        isPaused = true;
        pauseUI.SetActive(true);
        SetActiveComponents(false);
        Time.timeScale = 0f;
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused == false)
            {
                Pause();
            }else{
                Resume();
            }
        }
    }
}

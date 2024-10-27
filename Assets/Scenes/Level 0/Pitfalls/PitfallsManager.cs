using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PitfallsManager : MonoBehaviour
{
    public bool minigameStarted = false;
    [SerializeField] Collider2D blockingCollider;
    [SerializeField] Vector2 teleportPos;
    [SerializeField] float walkingSpeed;
    [SerializeField] float animationSpeed;
    [SerializeField] GameObject pitfallsUIprefab;
    [SerializeField] AudioClip gasp;
    private GameObject pitfallsUI;

    GameObject player;
    Animator animator;
    public float cursorValue = 0;
    public bool increases = true;
    [SerializeField] float cursorSpeed;
    RectTransform cursorLine;
    [SerializeField] GameObject noclipEffectPrefab;
    GameObject noclipEffect;
    [SerializeField] float maxCursorValue;
    [SerializeField] float minCursorValue;

    [Header("Right field")]
    [SerializeField] float minValue1;
    [SerializeField] float maxValue1;
    [Header("Left Field")]
    [SerializeField] float minValue2;
    [SerializeField] float maxValue2;

    [Header("Post Processing")]
    [SerializeField] Volume volume;
    [SerializeField] float vignetteIntensity;
    [SerializeField] float vignetteSmoothness;
    [SerializeField] float lensDistortionIntensity;
    [SerializeField] float chromaticAberrationIntensity;
    [SerializeField] float contrastIntensity;
    [SerializeField] float transitionSpeed;

    private float originalIntensity;
    private float originalSmoothness;
    private float originalDistortion;
    private float originalAberration;
    private float originalContrast;

    Vignette vignette;
    LensDistortion lensDistortion;
    ChromaticAberration chromaticAberration;
    ColorAdjustments colorAdjustments;

    void Start()
    {
        player = GameManager.instance.player;
        animator = player.GetComponent<Animator>();

        if (volume.profile.TryGet(out Vignette vignette1))
        {
            originalIntensity = vignette1.intensity.value;
            originalSmoothness = vignette1.smoothness.value;
            vignette = vignette1;
        }

        if (volume.profile.TryGet(out LensDistortion lensDistortion1))
        {
            originalDistortion = lensDistortion1.intensity.value;
            lensDistortion = lensDistortion1;
        }

        if (volume.profile.TryGet(out ChromaticAberration chromaticAberration1))
        {
            originalAberration = chromaticAberration1.intensity.value;
            chromaticAberration = chromaticAberration1;
        }

        if (volume.profile.TryGet(out ColorAdjustments colorAdjustments1))
        {
            originalContrast = colorAdjustments1.contrast.value;
        }
    }

    private void DoCursorMovement()
    {
        if (minigameStarted)
        {
            if (increases)
                cursorValue += cursorSpeed * Time.deltaTime;
            else
                cursorValue -= cursorSpeed * Time.deltaTime;

            if (cursorValue >= maxCursorValue)
                increases = false;

            if (cursorValue <= minCursorValue)
                increases = true;

            cursorLine.anchoredPosition = new Vector2(cursorValue, cursorLine.anchoredPosition.y);
        }
    }

    private void Fall()
    {
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        StopMinigame(false);
        animator.SetTrigger("Fall");
        SoundInstance.InstantiateOnTransform(gasp, transform, 2f, true, SoundInstance.Randomization.Low);
        noclipEffect = Instantiate(noclipEffectPrefab, GameManager.instance.uiCanvas.transform);
        StartCoroutine(TeleportDelay());
    }

    IEnumerator TeleportDelay()
    {
        yield return new WaitForSeconds(1);
        GameManager.instance.player.transform.position = teleportPos;
        player.GetComponent<PlayerMovement>().enabled = true;
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (cursorValue > maxValue1 || cursorValue < minValue1)
                Fall();
            else
                increases = false;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (cursorValue < maxValue2 || cursorValue > minValue2)
                Fall();
            else
                increases = true;
        }
    }

    void Update()
    {
        if (minigameStarted)
        {
            DoCursorMovement();
            if (cursorValue < minCursorValue || cursorValue > maxCursorValue)
                Fall();

            if (minigameStarted)
                HandleInput();
        }
    }

    private void StartMinigame()
    {
        if (player == null)
            return;

        PauseUIController.canPause = false;
        minigameStarted = true;
        Debug.Log("start pitfalls");
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, walkingSpeed);

        animator.SetBool("Balancing", true);
        animator.SetFloat("Balancing Velocity", animationSpeed);

        pitfallsUI = Instantiate(pitfallsUIprefab, GameManager.instance.uiCanvas.transform);
        cursorLine = pitfallsUI.transform.Find("Line").GetComponent<RectTransform>();
    }

    private void StopMinigame(bool enablePlayerMovement)
    {
        if (player == null)
            return;

        minigameStarted = false;
        PauseUIController.canPause = true;
        Debug.Log("stop pitfalls");

        Collider2D[] collider2Ds = gameObject.GetComponents<Collider2D>();
        foreach (Collider2D collider2D in collider2Ds)
            collider2D.enabled = false;

        blockingCollider.enabled = true;
        animator.SetBool("Balancing", false);

        player.GetComponent<PlayerMovement>().enabled = enablePlayerMovement;
        if (pitfallsUI != null)
            Destroy(pitfallsUI);
        enabled = false;
    }

    IEnumerator PostProccesingEffects(bool lerpToOriginal)
    {
        float timeElapsed = 0f;
        if (lerpToOriginal)
        {
            while (timeElapsed < transitionSpeed)
            {
                vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, originalIntensity, timeElapsed / transitionSpeed);
                vignette.smoothness.value = Mathf.Lerp(vignette.smoothness.value, originalSmoothness, timeElapsed / transitionSpeed);
                lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, originalDistortion, timeElapsed / transitionSpeed);
                chromaticAberration.intensity.value = Mathf.Lerp(chromaticAberration.intensity.value, originalAberration, timeElapsed / transitionSpeed);
                colorAdjustments.contrast.value = Mathf.Lerp(colorAdjustments.contrast.value, originalContrast, timeElapsed / transitionSpeed);

                timeElapsed += Time.deltaTime;

                yield return null;
            }
        }
        else
        {
            while (timeElapsed < transitionSpeed)
            {
                vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, vignetteIntensity, timeElapsed / transitionSpeed);
                vignette.smoothness.value = Mathf.Lerp(vignette.smoothness.value, vignetteSmoothness, timeElapsed / transitionSpeed);
                lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, lensDistortionIntensity, timeElapsed / transitionSpeed);
                chromaticAberration.intensity.value = Mathf.Lerp(chromaticAberration.intensity.value, chromaticAberrationIntensity, timeElapsed / transitionSpeed);
                colorAdjustments.contrast.value = Mathf.Lerp(colorAdjustments.contrast.value, contrastIntensity, timeElapsed / transitionSpeed);

                timeElapsed += Time.deltaTime;

                yield return null;
            }
        }

    }

    public void OnTriggerEnter2D()
    {
        StartMinigame();
    }

    public void OnTriggerExit2D()
    {
        StopMinigame(true);
    }
}

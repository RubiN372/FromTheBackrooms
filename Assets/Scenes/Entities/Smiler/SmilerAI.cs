using System.Collections;
using UnityEngine;
using Pathfinding;
using CodeMonkey.Utils;

public class SmilerAI : MonoBehaviour
{
    private enum State
    {
        Wandering,
        Chasing,
    }
    private State state;
    Vector3 target;

    #region Monster Settings
    [SerializeField] private float speed;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float nextWaypointDistance = 3f;
    [SerializeField] private float detectRange = 5f;
    [SerializeField] private SmilerAnimationController smilerAnimationController;
    private JumpscareController jumpscareController;
    [SerializeField] private Sprite jumpscareFaceSprite;
    [SerializeField] private Vector3 jumpscareMinScale;
    [SerializeField] private Vector3 jumpscareMaxScale;
    [SerializeField] private float jumpscareDuration;
    [SerializeField] private float jumpscareAfterDuration;
    [SerializeField] private AudioClip jumpscareSound;
    #endregion
    Path currentPath;
    int currentWaypoint = 0;
    // bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb;
    Vector2 direction = new(0, 0);
    private int playerLayer;
    public bool isChasing = false;
    private bool isCoroutineRunning = false;

    #region Start
    void Start()
    {
        playerLayer = LayerMask.GetMask("Player");
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        jumpscareController = GameManager.instance.jumpscareController;

        InvokeRepeating("UpdatePath", 0f, 0.2f);
    }
    #endregion
    private void Awake()
    {
        state = State.Wandering;
    }
    private void CheckForPlayers()
    {
        Collider2D[] playerColliders = Physics2D.OverlapCircleAll(transform.position, detectRange, playerLayer);

        if (playerColliders == null || playerColliders.Length <= 0)
        {
            isChasing = false;
            return;
        }

        for (int i = 0; i < playerColliders.Length; i++)
        {
            if (Vector2.Distance(transform.position, playerColliders[i].transform.position) <= detectRange && playerColliders[i].CompareTag("Player"))
            {
                currentPath = null;
                isChasing = true;
                smilerAnimationController.target = playerColliders[i].transform;
                target = playerColliders[i].transform.position;
                state = State.Chasing;
                return;
            }
        }

        isChasing = false;
    }

    private void CheckForFlashLights()
    {
        Collider2D[] flashlightColliders = Physics2D.OverlapCircleAll(transform.position, detectRange, LayerMask.GetMask("Item"));

        if (flashlightColliders == null || flashlightColliders.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < flashlightColliders.Length; i++)
        {
            if (flashlightColliders[i].gameObject.CompareTag("ThrowedFlashlight"))
            {
                isChasing = true;
                smilerAnimationController.target = flashlightColliders[i].transform;
                currentPath = null;
                target = flashlightColliders[i].transform.position;
                state = State.Chasing;
                return;
            }
        }
    }

    IEnumerator WanderingCoroutine()
    {
        while (state == State.Wandering && !isCoroutineRunning)
        {
            isCoroutineRunning = true;
            Vector3 position = transform.position + UtilsClass.GetRandomDir() * UnityEngine.Random.Range(3, 4);
            seeker.StartPath(rb.position, position, OnPathComplete);
            yield return new WaitForSeconds(3f);
            isCoroutineRunning = false;
        }
    }

    private void MakeDash()
    {

    }

    void UpdatePath()
    {
        switch (state)
        {
            case State.Wandering:
                StartCoroutine(WanderingCoroutine());
                CheckForPlayers();
                CheckForFlashLights();
                break;

            case State.Chasing:
                StopCoroutine(WanderingCoroutine());
                seeker.StartPath(rb.position, target, OnPathComplete);

                CheckForPlayers();
                CheckForFlashLights();

                if (isChasing == false)
                {
                    state = State.Wandering;
                }
                break;
        }
    }

    #region fUpdateMovement
    public Path GetPath()
    {
        return currentPath;
    }

    void OnPathComplete(Path newPath)
    {
        if (!newPath.error)
        {
            currentPath = newPath;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (currentPath == null)
            return;

        if (currentWaypoint >= currentPath.vectorPath.Count)
        {
            //reachedEndOfPath = true;
            return;
        }
        else
        {
            //reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)currentPath.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = acceleration * Time.deltaTime * direction.normalized;

        rb.AddForce(force);
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed);
        }

        float distance = Vector2.Distance(rb.position, currentPath.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    #endregion

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            jumpscareController.Jumpscare(jumpscareFaceSprite, jumpscareMinScale, jumpscareMaxScale, jumpscareDuration, jumpscareAfterDuration, jumpscareSound);
            gameObject.SetActive(false);
        }
    }

}

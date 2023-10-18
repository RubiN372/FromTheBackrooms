using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
using Unity.VisualScripting;

public class SmilerAI : MonoBehaviour
{
    private Transform target = null;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float nextWaypointDistance = 3f;
    [SerializeField] private float detectRange = 5f;
    [SerializeField] private SmilerAnimationController smilerAnimationController;
    [SerializeField] private JumpscareController jumpscareController;
    [SerializeField] private Sprite jumpscareFaceSprite;
    [SerializeField] private Vector3 jumpscareMinScale;
    [SerializeField] private Vector3 jumpscareMaxScale;
    [SerializeField] private float jumpscareDuration;
    [SerializeField] private float jumpscareAfterDuration;
    [SerializeField] private AudioClip jumpscareSound; 

    Path currentPath;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb;
    Vector2 direction = new(0, 0);
    private int playerLayer;
    public bool isChasing = false; 

    public Path GetPath()
    {
        return currentPath;
    }

    void Start()
    {
        playerLayer = LayerMask.GetMask("Player");
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.2f);
    }

   void UpdatePath()
{
    ContactFilter2D playerFilter;
    playerFilter.layerMask = playerLayer;

    Collider2D[] playerColliders = Physics2D.OverlapCircleAll(transform.position, detectRange, playerLayer);
    Collider2D[] flashlightColliders = Physics2D.OverlapCircleAll(transform.position, detectRange, LayerMask.GetMask("Item"));

    bool foundPlayer = false;  

    if (playerColliders != null && playerColliders.Length > 0)
    {
        for (int i = 0; i <= playerColliders.Length; i++)
        {
            Debug.Log(i);
            if (Vector2.Distance(transform.position, playerColliders[i].transform.position) <= detectRange && playerColliders[i].CompareTag("Player"))
            {
                currentPath = null;
                target = GameManager.instance.player.transform;
                seeker.StartPath(rb.position, target.position, OnPathComplete);
                foundPlayer = true;
                isChasing = true;
                smilerAnimationController.target = playerColliders[i].transform;
                break;
            }
        }
    }

    if (!foundPlayer)
    {
        target = null;  
        isChasing = false;
        smilerAnimationController.target = null;
    }
    
    if (flashlightColliders != null && flashlightColliders.Length > 0)
    {
        for (int i = 0; i < flashlightColliders.Length; i++)
        {
            if (flashlightColliders[i].gameObject.CompareTag("ThrowedFlashlight"))
            {
                isChasing = true;
                smilerAnimationController.target = flashlightColliders[i].transform;
                currentPath = null;
                target = flashlightColliders[i].gameObject.transform;
                seeker.StartPath(rb.position, target.position, OnPathComplete);
                break;
            }
        }
    }
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
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)currentPath.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = acceleration * Time.deltaTime * direction.normalized;

        rb.AddForce(force);
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

        float distance = Vector2.Distance(rb.position, currentPath.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.CompareTag("Player"))
        {
            jumpscareController.Jumpscare(jumpscareFaceSprite, jumpscareMinScale, jumpscareMaxScale, jumpscareDuration, jumpscareAfterDuration, jumpscareSound);
            gameObject.SetActive(false);
        }
    }
}

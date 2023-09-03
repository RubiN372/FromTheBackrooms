using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class SmilerAI : MonoBehaviour
{
    private Transform target = null;
    
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float nextWaypointDistance = 3f;
    [SerializeField] private float detectRange = 5f;
    [SerializeField] private float forgiveRange = 15f;

    Path currentPath;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb;
    Vector2 direction = new(0, 0);
    private int playerLayer;

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

    private void ResetVelocity()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
    }

    void UpdatePath()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 1f, 10);
        if (collider != null)
        {
            if (Vector2.Distance(transform.position, GameManager.instance.player.transform.position) <= detectRange)
            {
                target = GameManager.instance.player.transform;
                Debug.Log("Detected");
                seeker.StartPath(rb.position, target.position, OnPathComplete);
            }
            else{
                target = null;
                currentPath = null;
                ResetVelocity();
            }
        }
        else
        {
            target = null;
            currentPath = null;
            ResetVelocity();
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
}

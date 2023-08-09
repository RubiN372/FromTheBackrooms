using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;  

public class MonsterEnemyAI : MonoBehaviour
{
    public Transform target;
    

    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float nextWaypointDistance = 3f;
    [SerializeField] private float detectRange = 5f;
    Path currentPath;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb;
    Vector2 direction = new Vector2(0,0);

    

    public Path GetPath()
    {
        return currentPath;
    }
    
    


    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.2f); 
    }

    void UpdatePath()
    {
        RaycastHit2D hit = Physics2D.CircleCast(gameObject.transform.position, 12, direction, 12, 10 );
        if(hit.collider != null)
        {
            if(hit.distance <= detectRange)
            {
                target = GameManager.instance.player.transform;
            }else{
                target = null;
            }
        }

        if(seeker.IsDone() && target != null)
            seeker.StartPath(rb.position, target.position, OnPathComplete); 
        
    }

    void OnPathComplete(Path newPath)
    {
        if(!newPath.error)
        {
            currentPath = newPath;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if(currentPath == null)
            return;
        
        if(currentWaypoint >= currentPath.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)currentPath.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * acceleration * Time.deltaTime;

        rb.AddForce(force);
        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

        float distance = Vector2.Distance(rb.position, currentPath.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}

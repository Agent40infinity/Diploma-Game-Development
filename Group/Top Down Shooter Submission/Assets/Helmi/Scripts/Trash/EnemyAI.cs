using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public float speed = 400f;
    public float nextWaypointDistance = 3f; 

    public Transform target;
    public SpriteRenderer enemyGFX;

    private Path path;
    private int currentWayPoint = 0;
    private bool reachedEndOfPath = false;

    private Seeker seeker;
    private Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        InvokeRepeating("UpdatePath", 0f, 0.1f);        
    }
    
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rigid.position, target.position, OnPathComplete);
        }        
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;   
            currentWayPoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(path == null)
        {
            return;
        }

        if(currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rigid.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rigid.AddForce(force);

        float distance = Vector2.Distance(rigid.position, path.vectorPath[currentWayPoint]);

        if(distance < nextWaypointDistance)
        {
            currentWayPoint++;
        }

        if(target.position.x < transform.position.x)
        {
            enemyGFX.flipX = true;
        }

        if(target.position.x > transform.position.x)
        {
            enemyGFX.flipX = false;
        }

    }
}

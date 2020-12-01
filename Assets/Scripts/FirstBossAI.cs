﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FirstBossAI : MonoBehaviour
{
public UnityEngine.Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb;
    float count;
    bool stillCounting = true;
    float max = 3;
    bool attack = false;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().GetComponent<UnityEngine.Transform>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        
        
    }
    private void Update()
    {
        if (stillCounting) 
        { 
            count += Time.deltaTime; 
            if (count <= max)
            {
                stillCounting = false;
                count = 0;
                attack = true;
            } 
        }
        if (attack)
        {
            InvokeRepeating("UpdatePath", 0f, .5f);
        }
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    void UpdatePath()
    {
        

        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
        }

    }
    // Update is called once per frame
    
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    
    public void PlayerSeen()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
    
    
}
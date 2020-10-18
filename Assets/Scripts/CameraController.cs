﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject followTarget;
    private Vector3 targetPosition;
    public float moveSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
        if (GameObject.FindGameObjectsWithTag("MainCamera").Length > 1)
        {
            Object.Destroy(gameObject);

        }
        else
        {
            DontDestroyOnLoad(transform.gameObject);

        }
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}

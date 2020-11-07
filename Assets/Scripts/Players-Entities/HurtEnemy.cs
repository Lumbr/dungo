﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public int damage = 1;
    public GameObject damageBurst;
    public UnityEngine.Transform hitPoint;
    public PlayerController thePlayer;
    public float knockback = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GetComponentInParent<PlayerController>();
        knockback = thePlayer.knockback;
        damage = thePlayer.damage;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy") 
        {

            //other.gameObject.SetActive(false); 
            other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damage);
            Instantiate(damageBurst, hitPoint.transform.position, hitPoint.transform.rotation);
            
            

        }
    }

}
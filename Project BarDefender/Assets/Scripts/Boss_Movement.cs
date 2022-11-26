using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Boss_Movement : MonoBehaviour
{
    

    public float speed = 2.5f;
    public float attackRange = 3;
    
    public Transform player;
    Rigidbody2D rb;
    Boss_rotate boss;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        boss = GetComponent<Boss_rotate>();
    }


    void Update()
    {
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        boss.lookAtPlayer();

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            Attack();
        }
    }
    void Attack()
    {
        //look at BossAttack script
    }
    
}

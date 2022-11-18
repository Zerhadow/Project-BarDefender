using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [Min(0f)]
    public float despawnDistance;
    
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(transform.position.magnitude > despawnDistance)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        /*
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            // e.Fix();
        }
        */
        
        Destroy(gameObject);
    }
}

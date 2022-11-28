using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornMissle : MonoBehaviour
{
   public Transform target;
   public Rigidbody2D rb;
   public float angleChangingSpeed;
   public float movementSpeed;
    private void Awake()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    void FixedUpdate()
   {
       Vector2 direction = (Vector2)target.position - rb.position;
       direction.Normalize();
       float rotateAmount = Vector3.Cross(direction, transform.up).z;
       rb.angularVelocity = -angleChangingSpeed * rotateAmount;
       rb.velocity = transform.up * movementSpeed;
   }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDmg(10);
            Destroy(this);
        }
    }

}

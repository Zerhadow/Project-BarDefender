using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 100; //can change

    public GameObject deathEffect;

    public bool isInvulnerable = false;

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        health -= damage;

        if (health <= 50) //can change
        {
            GetComponent<Animator>().SetBool("IsEnragedd", true);

        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
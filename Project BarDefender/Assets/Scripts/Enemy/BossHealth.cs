using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : Units
{
    public int health = 100; //can change

    public GameObject deathEffect;

    public bool isInvulnerable = false;

    public void TakeDmg(int damage)
    {
        float originalHP = currHP;
        if (isInvulnerable)
            return;

        health -= damage;
        HPBar.SetHealth(currHP, originalHP);


        if (health <= 50) //can change
        {
            GetComponent<Animator>().SetBool("IsEnragedd", true);

        }

        if (health <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        //Die in some way
        //This method is meant to be overwritten
        Destroy(gameObject);
        Debug.Log(transform.name + " died");
    }


}
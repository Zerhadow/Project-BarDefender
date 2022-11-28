using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : Units
{
    public GameObject deathEffect;

    public bool isInvulnerable = false;

    public override void TakeDmg(int damage)
    {
        float originalHP = currHP;
        if (isInvulnerable)
            return;
        Debug.Log("Hurt");

        currHP -= damage;
        dmg = Mathf.Clamp(dmg, 0, int.MaxValue);
        HPBar.SetHealth(currHP, originalHP);


        if (currHP <= 50) //can change
        {
            GetComponentInChildren<Animator>().SetBool("IsEnraged", true);

        }

        if (currHP <= 0)
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
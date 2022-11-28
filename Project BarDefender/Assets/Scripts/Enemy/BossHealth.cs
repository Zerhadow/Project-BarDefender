using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : Units
{
    public GameObject deathEffect;
    private bool Transformed = false;
    public bool isInvulnerable = false;
    public GameObject projectilePrefab;

    public override void TakeDmg(int damage)
    {
        float originalHP = currHP;
        if (isInvulnerable)
            return;
        Debug.Log("Hurt");

        currHP -= damage;
        dmg = Mathf.Clamp(dmg, 0, int.MaxValue);
        HPBar.SetHealth(currHP, originalHP);


        if (currHP <= maxHP/2) //can change
        {
            GetComponentInChildren<Animator>().SetBool("Transform", true);

            if (!Transformed)
                StartCoroutine(refreshTimer());
        }

        if (currHP <= 0)
        {
            SceneManager.LoadScene("Cutscene_3_EndDialog");
            Die();
        }
    }

    public override void Die()
    {
        //Die in some way
        //This method is meant to be overwritten
        SceneManager.LoadScene("Cutscene_3_End Dialog");
        //Destroy(gameObject);
        Debug.Log(transform.name + " died");
    }

    IEnumerator refreshTimer()
    {
        isInvulnerable = true;
        Debug.Log(transform.name + " died");
        yield return new WaitForEndOfFrame();
        float originalHP = currHP;
        currHP = maxHP;

        //this.transform.position -= speed

        yield return new WaitForSeconds(2);
        HPBar.SetHealth(currHP, originalHP);

        yield return new WaitForEndOfFrame();
        Transformed = true;
        isInvulnerable = false;
    }


}
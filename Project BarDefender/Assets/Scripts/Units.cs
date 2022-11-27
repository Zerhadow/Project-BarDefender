using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units : MonoBehaviour
{
    public int maxHP = 100;
    public float currHP;
    public int dmg;
    public HealthBar HPBar;
    //public AudioSource dmgSound;
    public bool invincible = false;
    public float invcibilityDuration = 0.001f;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        currHP = maxHP;
        // Debug.Log("currHP: " + currHP);        
        HPBar.SetMaxHealth(maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void TakeDmg(int dmg) {
        // Player or some mob take dmg
        // method meant to be overwritten
        Debug.Log(transform.name + "took dmg");
    }

    public virtual void Die() {
        //Die in some way
        //This method is meant to be overwritten
        Debug.Log(transform.name + " died");
    }

    public IEnumerator Invincible(float duration){
        invincible = true;
        while(duration > 0){
            duration -= Time.deltaTime;
            yield return null;
        }
        invincible = false;
    }
        
}

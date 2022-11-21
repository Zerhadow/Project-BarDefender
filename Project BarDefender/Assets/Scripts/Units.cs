using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units : MonoBehaviour
{
    public int maxHP = 100;
    public float currHP {get; private set;}
    public int dmg;
    //public HUDHealth HPBar;
    //public AudioSource dmgSound;
    public bool invincible = false;
    float invcibilityDuration = 0.001f;
    
    // Start is called before the first frame update
    void Start()
    {
        currHP = maxHP;
        //HPBar.SetMaxHealth(maxHP);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDmg(int dmg) {
        if(!invincible){
            float originalHP = currHP;
            dmg = Mathf.Clamp(dmg, 0, int.MaxValue);
            currHP -= dmg;
            if(gameObject.tag != "Player"){
                //GameManager.comboManager.IncrementCombo();
                //CameraShaker.Instance.ShakeOnce(0.5f + shakeMultiplier*dmg, 0.5f, 0.3f, 0.15f);
            }

            if(gameObject.tag == "Player"){
                //CameraShaker.Instance.ShakeOnce(1.3f,3f,0.2f,0.4f);
                //AudioManager.PlaySound("PlayerDamage");
            }

            //HPBar.SetHealth(currHP, originalHP);
            // if(hitEffect != null)
            //     Instantiate(hitEffect, transform.position, Quaternion.identity);

            if (currHP <= 0) {
                Die();
            }
            StartCoroutine(Invincible(invcibilityDuration));
        }
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

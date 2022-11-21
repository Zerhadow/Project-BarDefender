using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public int maxHP = 100;
    public float currHP {get; private set;}
    public int dmg;
    //public HUDHealth HPBar;
    //public AudioSource dmgSound;


    
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Units
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Die() {
        //Die in some way
        //This method is meant to be overwritten
        Destroy(gameObject);
        Debug.Log(transform.name + " died");
    }
}

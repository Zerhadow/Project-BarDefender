using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : Units
{
    public override void Die() {
        base.Die();
        // AudioManager.PlaySound("EnemyDeath");
        Destroy(this.gameObject);
        //drop ingredient function based on monster rarity
        Debug.Log(transform.name + " died");
    }
}

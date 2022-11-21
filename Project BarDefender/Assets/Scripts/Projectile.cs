using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [Min(0f)]
    public float despawnDistance;
    GameObject enemy;
    public GameObject shooter;
    Units shooterStat;

    
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        shooterStat = shooter.GetComponent<Units>();

    }

    void Update()
    {
        if(transform.position.magnitude > despawnDistance)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Bullet hit " + other.name);
        if(this.tag == "PlayerBullet") {
            if(other.tag == "Enemy") {
                Units enemyStat = other.gameObject.GetComponent<Units>();
                enemyStat.TakeDmg(shooterStat.dmg);
                Debug.Log("Enemy HP: " + enemyStat.currHP);        
                //Instantiate(particles,transform.position,Quaternion.identity);
                //AudioManager.PlaySound("BulletCollide");
                Destroy(gameObject);
            }
        } else if(this.tag == "EnemyBullet"){
            if(other.tag == "Player") {
                Units playerStat = other.gameObject.GetComponent<Units>();
                if(!playerStat.invincible){
                    //Instantiate(particles,transform.position,Quaternion.identity);
                
                    playerStat.TakeDmg(shooterStat.dmg);
                    Debug.Log("Player HP: " + playerStat.currHP);        
                    Destroy(gameObject);
                }
            }
        }
    }

}

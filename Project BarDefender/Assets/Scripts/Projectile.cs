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
    Transform player;
    public bool isThorn = false;

    
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

        if (isThorn)
        {
            Vector2 target = new Vector2(player.position.x, player.position.y);
            //Keep speed at zero to not have boss move
            Vector2 newPos = Vector2.MoveTowards(rigidbody2d.position, target, 2 * Time.fixedDeltaTime);
            rigidbody2d.MovePosition(newPos);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void OnTriggerEnter2D(Collider2D other) {
        // Debug.Log("Bullet hit " + other.name);
        if(this.tag == "PlayerBullet") {
            if(other.tag == "Enemy") {
                Units enemyStat = other.gameObject.GetComponent<Units>();
                enemyStat.TakeDmg(shooterStat.dmg);
                // Debug.Log("Enemy HP: " + enemyStat.currHP);        
                //Instantiate(particles,transform.position,Quaternion.identity);
                //AudioManager.PlaySound("BulletCollide");
                Destroy(gameObject);
            } else if(other.tag == "Player") {
                Debug.Log("collide w player");
            }
        } else if(this.tag == "EnemyBullet"){
            if(other.tag == "Player") {
                Units playerStat = other.gameObject.GetComponent<Units>();
                if(!playerStat.invincible){
                    //Instantiate(particles,transform.position,Quaternion.identity);
                
                    player.GetComponent<PlayerController>().TakeDmg(15);
                    
                    Debug.Log("Player HP: " + playerStat.currHP);        
                    Destroy(gameObject);
                }
            }
        }
    }

}

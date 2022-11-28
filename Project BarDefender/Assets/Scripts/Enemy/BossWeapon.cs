using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public int attackDamage = 30;
    public int madAttackDamage = 10;
    [SerializeField] public Transform AttackPoint;

    public Vector3 attackOffset;
    public float attackRange = 3f;
    public LayerMask attackMask;
    public Vector3 attackPos;
    public Animator _myAnimator;
    public PlayerController _player;

    private void Awake()
    {
        if (!_player)
        {
            _player = FindObjectOfType<PlayerController>();
        }
    }


    public void Attack()
    {

        //StartCoroutine(AttackEnum());
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, attackMask);
        foreach (Collider2D player in hitPlayer)
        {
    
            player.GetComponent<PlayerController>().TakeDmg(attackDamage);
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-4f, -1f), ForceMode2D.Impulse);
        }

        
            //PlayerHealth is what'll be changed out for what name of thte actual player health is
    }
    void OnDrawGizmosSelected()
    {
        //Vector3 pos = transform.position; pos += transform.right * attackOffset.x; pos += transform.up * attackOffset.y;
        //Gizmos.DrawWireSphere(pos, attackRange);
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }

}
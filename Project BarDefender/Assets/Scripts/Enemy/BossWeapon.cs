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

    private void Update()
    {
        Attack();
    }

    public void Attack()
    {
        //StartCoroutine(AttackEnum());
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, attackMask);
        foreach (Collider2D player in hitPlayer)
        {
            // Debug.Log("We hit " + enemy.name);
            Debug.Log("Hit Player!");
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
    //go to the "attack animation" in aniamation, find the frame that'll trigger the event,
    // add event, got to function in the inspector, select "Attack" (or the equivlent to it)

    //IEnumerator AttackEnum()
    //{
    //    attackPos = transform.position;
    //    yield return new WaitForEndOfFrame();
    //    yield return new WaitForSeconds(
    //        _myAnimator.GetAnimatorTransitionInfo(0).duration);
    //
    //    attackPos += transform.right * attackOffset.x;
    //    yield return new WaitForEndOfFrame();
    //    yield return new WaitUntil(() =>
    //        _myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f);
    //
    //    attackPos += transform.up * attackOffset.y;
    //    //isFlexing = false;
    //}
}
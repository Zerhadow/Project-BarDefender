using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public int attackDamage = 0;
    public int madAttackDamage = 0;

    public Vector3 attackOffset;
    public float attackRange = 3f;
    public LayerMask attackMask;
    public Vector3 attackPos;
    public Animator _myAnimator;

    private void Update()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos, attackRange);
    }

    public void Attack()
    {
        StartCoroutine(AttackEnum());
        
        Collider2D colInfo = Physics2D.OverlapCircle(attackPos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerController>().TakeDmg(attackDamage);
            //PlayerHealth is what'll be changed out for what name of thte actual player health is
        }
    }
    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position; pos += transform.right * attackOffset.x; pos += transform.up * attackOffset.y;
        Gizmos.DrawWireSphere(pos, attackRange);
    }
    //go to the "attack animation" in aniamation, find the frame that'll trigger the event,
    // add event, got to function in the inspector, select "Attack" (or the equivlent to it)

    IEnumerator AttackEnum()
    {
        attackPos = transform.position;
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(
            _myAnimator.GetAnimatorTransitionInfo(0).duration);

        attackPos += transform.right * attackOffset.x;
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() =>
            _myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f);

        attackPos += transform.up * attackOffset.y;
        //isFlexing = false;
    }
}
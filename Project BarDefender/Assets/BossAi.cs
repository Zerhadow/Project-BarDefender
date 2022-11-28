using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAi : StateMachineBehaviour
{

    public float speed = 0f;
    public float attackRange = 5f;
    Transform playerTransform;
    Rigidbody2D rb;
    BossRotate boss;
    private void Awake()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform;
    }
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int LayerIndex)
    {
        if (!rb)
        rb = animator.GetComponentInParent<Rigidbody2D>();
        if (!boss)
        boss = animator.GetComponent<BossRotate>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int LayerIndex)
    {
        //boss.LookAtPlayer(); //Don't add BossRotate to boss prerfab to not have boss rotate
        if (playerTransform)
        {
            Vector2 target = new Vector2(playerTransform.position.x, rb.position.y); //Keep speed at zero to not have boss move
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            if (Vector2.Distance(playerTransform.position, rb.position) <= attackRange)
            {
                animator.SetTrigger("Attack"); //There's more than one attack, make sure to clarify
            }
            if (Vector2.Distance(playerTransform.position, rb.position) > attackRange)
            {
                animator.SetTrigger("GemAttack");
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int LayerIndex)
    {
        animator.ResetTrigger("Attack"); //There's more than one attack, make sure to clerify
        animator.ResetTrigger("GemAttack");
    }


}
//code for range attack (gem attack) is opposite of meleeAttack,
//if (Vector2.Distance(player.position, rb.position) <= attackRange)

// to enrange, in the animator tab go to paramator->bollean, name it "isenrage", 
//in the transistion from idle to enrage idle, set condition (bottom of inspector) to "isenrage" and true.

//the stage2 form, add behaviour "bossAI" on the stage2 idle animator tab

//add an enraged attack (assume the boss collides with the player)

//add a behavior to the stage 2 transformation, bossEnrage,
//IMPOARTANT IMPOARTANT
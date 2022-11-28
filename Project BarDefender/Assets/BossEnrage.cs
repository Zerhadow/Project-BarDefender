using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnrage : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int LayerIndex)
    {
        animator.GetComponentInParent<BossHealth>().isInvulnerable = true;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int LayerIndex)
    {

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int LayerIndex)
    {
        animator.GetComponentInParent<BossHealth>().isInvulnerable = false;
    }
}
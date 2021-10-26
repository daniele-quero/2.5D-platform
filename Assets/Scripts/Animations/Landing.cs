using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : StateMachineBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.GetComponentInParent<PlayerMovement>().Velocity = Vector3.zero;
        
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.GetComponentInParent<CharacterController>().enabled = false;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.GetComponentInParent<CharacterController>().enabled = true;
    }

}

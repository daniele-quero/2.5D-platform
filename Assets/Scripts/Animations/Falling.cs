using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : StateMachineBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        CharacterController controller = animator.GetComponentInParent<CharacterController>();
        if (controller != null && controller.isGrounded)
        {
            if (animator.GetComponentInParent<PlayerMovement>().Velocity.y < -50f)
                animator.GetComponentInParent<Death>().DeathLogic();
            else
                animator.SetTrigger("onLanding");
        }
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.GetComponentInParent<CharacterController>().enabled = true;
    }
}

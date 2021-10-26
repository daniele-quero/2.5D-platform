using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : StateMachineBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        CharacterController controller = animator.GetComponentInParent<CharacterController>();
        if (controller != null && controller.isGrounded)
            animator.SetTrigger("onLanding");
    }
}

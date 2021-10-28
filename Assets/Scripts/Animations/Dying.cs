using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dying : StateMachineBehaviour
{
    private PlayerMovement _pm;
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (animator.transform.parent.position == _pm.StartingPosition)
            animator.SetTrigger("onRevive");
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.GetComponentInParent<CharacterController>().enabled = false;
        _pm = animator.GetComponentInParent<PlayerMovement>();
        animator.GetComponentInParent<Death>().StartCoroutine("SlowRespawnRoutine");
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _pm.Velocity = Vector3.zero;
        animator.GetComponentInParent<CharacterController>().enabled = true;
    }




}

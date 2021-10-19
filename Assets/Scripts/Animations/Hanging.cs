using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hanging : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKey(KeyCode.E))
        {
            animator.SetTrigger("onLedgeClimb");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("onLedgeClimb");
        animator.SetFloat("speed", 0f);
        animator.ResetTrigger("onLedgeGrab");
    }
}

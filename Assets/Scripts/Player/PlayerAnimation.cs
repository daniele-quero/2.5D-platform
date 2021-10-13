using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;


    public void SetRunningAnimationParameters(Vector3 worldHorizontalSpeed)
    {
        animator.ResetTrigger("onJump");
        animator.SetFloat("speed", worldHorizontalSpeed.magnitude);
        animator.SetFloat("fallingSpeed", 0);
    }

    public void SetJumpAnimationParameters()
    {
        animator.SetTrigger("onJump");
        animator.SetFloat("speed", 0f);
    }

    public void SetFallingAnimationParameters(float yvel)
    {
        animator.SetFloat("fallingSpeed", yvel);
    }

}

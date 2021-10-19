using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingLedge : StateMachineBehaviour
{
    private Transform _target;
    private Transform _player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = animator.transform.parent;
        _target = _player.GetComponent<LedgeGrab>().ClimbTransform;
        animator.applyRootMotion = true;
        animator.ResetTrigger("onLedgeClimb");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Camera.main.transform.LookAt(animator.transform);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player.position = _target.position;
        _player.GetComponent<CharacterController>().enabled = true;
        animator.transform.localPosition = Vector3.zero;
        animator.applyRootMotion = false;
        Camera.main.transform.localPosition = Camera.main.GetComponent<CameraStats>().startPosition;
        Camera.main.transform.localRotation = Camera.main.GetComponent<CameraStats>().startRotation;
    }
}

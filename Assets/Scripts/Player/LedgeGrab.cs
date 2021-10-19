using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrab : MonoBehaviour
{
    //private bool _isHanging = false;
    [SerializeField]
    private Transform _climbTransform;

    public Transform ClimbTransform { get => _climbTransform; }

    public void GrabLedge(Transform grabTransform)
    {
        CharacterController controller = GetComponent<CharacterController>();
        controller.enabled = false;

        PlayerAnimation pa = GetComponent<PlayerAnimation>();
        pa.SetGrabLedgeAnimationParameters();

        transform.position = grabTransform.position;
        _climbTransform = grabTransform.parent.GetChild(1);
    }
}

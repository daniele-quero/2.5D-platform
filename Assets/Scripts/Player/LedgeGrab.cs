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
        Vector3 look = (grabTransform.parent.position.z - transform.position.z) * Vector3.forward;
        transform.GetChild(0).rotation = Quaternion.LookRotation(look, Vector3.up);
        _climbTransform = grabTransform.parent.GetChild(1);
    }

    public void TemporaryDeactivateLedge()
    {
        StartCoroutine(DeactivateLedgeRoutine());
    }

    private IEnumerator DeactivateLedgeRoutine()
    {
        var ledgeColliders = _climbTransform.GetComponentsInParent<BoxCollider>();
        ledgeColliders[ledgeColliders.Length - 1].enabled = false;
        yield return new WaitForSeconds(1.5f);
        ledgeColliders[ledgeColliders.Length - 1].enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "movable"
            && Vector3.Distance(transform.position, other.transform.position) <= 0.2f)
        {
            other.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
            other.tag = "unmovable";
            other.attachedRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            GetComponentInChildren<MeshRenderer>().enabled = false;
            UIManager.Instance.SetElevatorText("VICTORY!!!");
        }
    }
}

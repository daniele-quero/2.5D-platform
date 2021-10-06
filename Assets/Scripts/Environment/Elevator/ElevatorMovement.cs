using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour
{
    [SerializeField]
    private Transform _up, _down;

    private MovingPlatform _mp;

    private void Start()
    {
        _mp = GetComponent<MovingPlatform>();
    }
    public void CallElevator()
    {
        _mp.waypoints[0] = _down;
        _mp.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _mp.waypoints[0] = _up;
            _mp.speed = 4f;
        }
    }
}

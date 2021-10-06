using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public List<Transform> waypoints;
    public float idleTime = 1.5f;
    public float speed = 4f;
    public bool waitOnWaypoint = false;
    private int _currentTargetId = -1;
    private int _direction = -1;
    private WaitForSeconds _idleOnLimit;
    private WaitForSeconds _idleOnWaypoint;

    void Start()
    {
        _idleOnLimit = new WaitForSeconds(idleTime);
        _idleOnWaypoint = new WaitForSeconds(idleTime * 0.25f);

        _currentTargetId = 0;
    }

    private void FixedUpdate()
    {
        if (WaypointsGood())
        {
            if (transform.position == waypoints[_currentTargetId].position)
            {
                _currentTargetId = NextTarget();

                if (OnWaypointsLimits())
                    StartCoroutine(StopPlatformRoutine(_idleOnLimit));

                else if (waitOnWaypoint)
                    StartCoroutine(StopPlatformRoutine(_idleOnWaypoint));
            }

            MovePlatform(_currentTargetId);
        }
    }

    private IEnumerator StopPlatformRoutine(WaitForSeconds wait)
    {
        float oldSpeed = speed;
        speed = 0;
        yield return wait;
        speed = oldSpeed;
    }

    private void MovePlatform(int id)
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[id].position, speed * Time.deltaTime);
    }

    private int NextTarget()
    {
        int next = _currentTargetId + _direction;
        if (OnWaypointsLimits())
            _direction *= -1;

        _currentTargetId += _direction;

        return IdGood(_currentTargetId) ? _currentTargetId : _currentTargetId - _direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            other.transform.SetParent(null);
    }

    private void OnEnable()
    {
        Start();
    }

    #region Checks
    private bool WaypointsGood()
    {
        return waypoints != null && waypoints.Count > 0;
    }

    private bool OnWaypointsLimits()
    {
        return transform.position == waypoints[0].position
            || transform.position == waypoints[waypoints.Count - 1].position;
    }

    private bool IdGood(int id)
    {
        return id >= 0 && id < waypoints.Count;
    }
    #endregion
}

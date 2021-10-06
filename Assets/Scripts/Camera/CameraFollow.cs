using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Vector2 _offset;
    private Vector2 _threshold;

    [SerializeField]
    private PlayerMovement _playerMovement;
    [SerializeField]
    private float _speed;

    void Start()
    {
        _threshold = CalculateThreshold();
    }

    void Update()
    { 
        FollowPlayer();
    }

    private Vector2 CalculateThreshold()
    {
        return new Vector2(
            Camera.main.orthographicSize * Camera.main.aspect - _offset.x,
             Camera.main.orthographicSize - _offset.y);
    }

    private void FollowPlayer()
    {
        Vector3 position = transform.position;
        if (Mathf.Abs(transform.position.x - _playerMovement.transform.position.x) > _threshold.x)
            position.x = _playerMovement.transform.position.x;

        if (Mathf.Abs(transform.position.y - _playerMovement.transform.position.y) > _threshold.y)
            position.y = _playerMovement.transform.position.y;

        float speed = Mathf.Max(_speed, _playerMovement.Velocity.magnitude) * 3f;
        transform.position = Vector3.MoveTowards(transform.position, position, _speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        _threshold = CalculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(_threshold.x * 2, _threshold.y * 2, 0.5f));
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f, _g = 9.81f, _jumpspeed = 8f;
    private float _groundedVelocity = -0.001f;

    [SerializeField]
    private Vector3 _startingPosition, _velocity = Vector3.zero;
    private Vector3 _hitWallNormal;
    Vector3 _lastNonZeroSpeed = Vector3.forward;

    private PlayerAnimation _pa;
    private CharacterController _controller;

    [SerializeField]
    private Transform _modelTransform;

    private bool _canDoubleJump = false;
    private bool _canWallJump = false;
    public bool isGrounded;

    public Vector3 Velocity { get => _velocity; set => _velocity = value; }
    public Vector3 HitWallNormal { get => _hitWallNormal; set => _hitWallNormal = value; }
    public bool CanWallJump { get => _canWallJump; set => _canWallJump = value; }
    public Vector3 StartingPosition { get => _startingPosition; set => _startingPosition = value; }

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _controller.minMoveDistance = 0;
        _pa = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        Move();
        isGrounded = _controller.isGrounded;
    }

    private void Move()
    {
        _velocity.z = Input.GetAxis("Horizontal") * _speed;
        if (_controller.isGrounded)
        {

            _velocity.y = _groundedVelocity;

            Vector3 worldHorizontalSpeed = Vector3.forward * _velocity.z + Vector3.right * _velocity.x;
            _lastNonZeroSpeed = worldHorizontalSpeed != Vector3.zero ? worldHorizontalSpeed : _lastNonZeroSpeed;

            _pa.SetFallingAnimationParameters(0);
            _pa.SetRunningAnimationParameters(worldHorizontalSpeed);
            _modelTransform.rotation = Quaternion.LookRotation(_lastNonZeroSpeed, Vector3.up);

            _canDoubleJump = false;
            _canWallJump = false;

            Jump();
        }
        else
        {
            _velocity.y += -0.5f * _g * Time.deltaTime;

            if (_canDoubleJump && !_canWallJump)
                Jump();

            else if (_canWallJump)
                WallJump();

            _pa.SetFallingAnimationParameters(_velocity.y);
        }

        if (_controller.enabled)
            _controller.Move(_velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _velocity.y = _jumpspeed;
            _canDoubleJump = !_canDoubleJump;
            _pa.SetJumpAnimationParameters();
        }
    }

    private void WallJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _velocity = (Vector3.up + _hitWallNormal * 1.04f).normalized * _jumpspeed * 0.6f;
            _canWallJump = !_canWallJump;
        }
    }

    public void Respawn()
    {
        Debug.Log("Resapwn");
        _velocity = Vector3.zero;
        transform.position = _startingPosition;
    }

    public void PushMovables(ControllerColliderHit hit)
    {
        Rigidbody rg;
        if ((rg = hit.gameObject.GetComponent<Rigidbody>()) == null)
            return;

        rg.velocity = hit.moveDirection / rg.mass;
    }

}

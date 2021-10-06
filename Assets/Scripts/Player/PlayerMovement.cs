using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private float _g = 9.81f;
    [SerializeField]
    private float _jumpspeed = 8f;
    [SerializeField]
    private Vector3 _startingPosition;

    private CharacterController _controller;
    private bool _canDoubleJump = false;
    private float _groundedVelocity = -0.00001f;

    private Vector3 _hitWallNormal;
    private bool _canWallJump = false;
    [SerializeField]
    private Vector3 _velocity = Vector3.zero;

    public Vector3 Velocity { get => _velocity; }

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _controller.minMoveDistance = 0;
    }


    void Update()
    {
        Move();
    }

    private void Move()
    {


            

        if (_controller.isGrounded)
        {
            _canWallJump = false;
                _velocity.x = Input.GetAxis("Horizontal") * _speed;
                _canDoubleJump = false;
            _velocity.y = _groundedVelocity;
            Jump();
        }
        else
        {
            //_velocity.y += -_g;
            _velocity.y += -0.5f * _g * Time.deltaTime;
            if (_canDoubleJump && !_canWallJump)
                Jump();

            else if (_canWallJump)
                WallJump();
        }


        _controller.Move(_velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _velocity.y = _jumpspeed;
            _canDoubleJump = !_canDoubleJump;
        }
    }

    private void WallJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _velocity = (Vector3.up + _hitWallNormal*1.04f).normalized * _jumpspeed *0.6f;
            _canWallJump = !_canWallJump;
        }
    }

    public void Respawn()
    {
        _velocity = Vector3.zero;
        transform.position = _startingPosition;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.normal.y == -1f)
        {
            _velocity.y = -0.6f;
            _velocity.x *= 0.9f;
        }

        switch (hit.gameObject.tag)
        {
            case "jumpableWall":
                {
                    _hitWallNormal = hit.normal;
                    _canWallJump = true;
                    break;
                }
            case "movable":
                {
                    PushMovables(hit);
                    break;
                }
            default:
                break;
        }

    }

    private void PushMovables(ControllerColliderHit hit)
    {
        Rigidbody rg;
        if ((rg = hit.gameObject.GetComponent<Rigidbody>()) == null)
            return;

        rg.velocity = hit.moveDirection / rg.mass;
    }

}

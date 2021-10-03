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

    private CharacterController _controller;
    private bool _canDoubleJump = false;

    [SerializeField]
    private Vector3 _velocity = Vector3.zero;

    public Vector3 Velocity { get => _velocity; set => _velocity = value; }

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
        _velocity.x = Input.GetAxis("Horizontal") * _speed;

        if (_controller.isGrounded)
        {
            _canDoubleJump = false;
            _velocity.y = -0.001f;
            Jump();
        }
        else
        {
            //_velocity.y += -_g;
 
            if (_canDoubleJump)
                Jump();
        }

        
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _velocity = Vector3.up * _jumpspeed;
            _canDoubleJump = !_canDoubleJump;
        }
    }

}

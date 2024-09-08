
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Vector2 _inputVector;
    private Vector2 _velocityVector;
    public float powerScaler;
    private Animator _animator;
    public float jumpPower;
    public float climbPower;
    private CapsuleCollider2D _collider;
    private LayerMask _collidingLayer;
    private LayerMask _climbingLayer;
    private float _playerGravity;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider2D>();
        _playerGravity = _rigidbody2D.gravityScale;
        _collidingLayer = LayerMask.GetMask("Platform");
        _climbingLayer = LayerMask.GetMask("Climbing");
        powerScaler = 5.3f;
        jumpPower = 9.3f;
        climbPower = 4.5f;
    }

    private void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    private void FlipSprite()
    {
       bool isMoving = Mathf.Abs(_rigidbody2D.velocity.x) > Mathf.Epsilon;
        if (isMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(_rigidbody2D.velocity.x), 1f); 
        }
    }

    private void OnJump(InputValue input)
    {
        if (input.isPressed && _collider.IsTouchingLayers(_collidingLayer))
        {
            _rigidbody2D.velocity += new Vector2(0f, jumpPower);
        }
    }
    private void Run()
    {
        _velocityVector = new Vector2(_inputVector.x * powerScaler , _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = _velocityVector;
        bool isMoving = Mathf.Abs(_rigidbody2D.velocity.x) > Mathf.Epsilon;
        _animator.SetBool("IsRunning", isMoving);   
    }
    void OnMove(InputValue inputValue)
    {
        _inputVector = inputValue.Get<Vector2>();
    }

    private void ClimbLadder()
    {
        if (_collider.IsTouchingLayers(_climbingLayer))
        {
            _rigidbody2D.gravityScale = 0f;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,_inputVector.y * climbPower);
            bool isMoving = Mathf.Abs(_rigidbody2D.velocity.y) > Mathf.Epsilon;
                _animator.SetBool("IsClimbing",isMoving);
                
        }
        else
        {
            _rigidbody2D.gravityScale = _playerGravity;
            _animator.SetBool("IsClimbing",false);
        }
    }
}

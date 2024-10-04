using System.Collections;
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
    private readonly float _rayLength = 0.5f;
    private Vector2 _boxSize;
    private float _angle = 0f;
    private PlayerInput _playerInput;
    private SpriteRenderer _spriteRenderer;
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    public GameObject _bullet;
    public GameObject _gun;
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int IsClimbing = Animator.StringToHash("IsClimbing");

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider2D>();
        _boxSize = new Vector2(0.35f, 0.2f);
        _playerGravity = _rigidbody2D.gravityScale;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        _collidingLayer = LayerMask.GetMask("Platform","Hazards");
        _playerInput = GetComponent<PlayerInput>();
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
    private bool IsGrounded(int mode)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, _boxSize, _angle, Vector2.down, _rayLength, _collidingLayer);

    
        if (mode == 1)
        {
            Debug.Log(hit.collider!=null);
            return hit.collider != null;
        }

        if (mode == 2)
        {
            if (hit.collider == null)
            {
                Debug.Log("Null Hazard collider");
                return false; 
            }
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Hazards"))
            {
                Debug.Log("Hazard Collider Not Null");
                return true;
            }
        }
        return false;
    }
    


    private void PlayerDeath()
    {
        _playerInput.enabled = false;
        gameObject.layer  = LayerMask.NameToLayer("NoCollidePlayer");
        _animator.SetTrigger(IsDead); 
        StartCoroutine(SetVisibility(1.7f));
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.CompareTag("Enemy") || IsGrounded(2)))
        {
            // Debug.Log("PlayerDeath Entered");
            PlayerDeath();
        }

        if (other.gameObject.CompareTag("Bounce"))
        {
            AudioManager.Instance.PlayEffect(2);
        }
    }

    private IEnumerator SetVisibility(float delay)
    {
        yield return new WaitForSeconds(delay);
        _spriteRenderer.enabled = false;
        GameSession.Instance.ProcessPlayerDeath();
        
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
        if (input.isPressed && IsGrounded(1))
        {
            AudioManager.Instance.PlayEffect(1);
            _rigidbody2D.velocity += new Vector2(0f, jumpPower);
        }
    }
    private void OnFire(InputValue input)
    {
        if (input.isPressed)
        {
            AudioManager.Instance.PlayEffect(3);
            Instantiate(_bullet, _gun.transform.position, Quaternion.identity);
            // Debug.Log("Bullet fired");
        }
    }
    private void Run()
    {
        _velocityVector = new Vector2(_inputVector.x * powerScaler , _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = _velocityVector;
        bool isMoving = Mathf.Abs(_rigidbody2D.velocity.x) > Mathf.Epsilon;
        _animator.SetBool(IsRunning, isMoving);   
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
                _animator.SetBool(IsClimbing,isMoving);
                
        }
        else
        {
            _rigidbody2D.gravityScale = _playerGravity;
            _animator.SetBool("IsClimbing",false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private LayerMask _collidingLayer;
    private float _rayLength = 0.40f;
    private float _flipCoolDown=0.2f;
    private bool _canFlip = true;
    private Vector2 _RayDirection = Vector2.right;

    public Vector2 moveSpeed= new Vector2(1.3f,0f);
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collidingLayer = LayerMask.GetMask("Platform");
    }

    private void FacingWallFlip()
    {
            Debug.Log("Collided");
            FlipSpeed();
            transform.localScale = new Vector2(transform.localScale.x*-1f, 1f);
            _RayDirection *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.velocity = moveSpeed;
        // Debug.DrawRay(transform.position, _RayDirection *_rayLength, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _RayDirection, _rayLength, _collidingLayer);
        if (hit.collider != null && _canFlip)
        {
            FacingWallFlip();
            StartCoroutine(FlipCoolDown());
        }
    }

    private void FlipSpeed()
    {
            moveSpeed = -moveSpeed;
    }

    private IEnumerator FlipCoolDown()
    {
        _canFlip = false;
        yield return new WaitForSeconds(_flipCoolDown);
        _canFlip = true;
    }
    
}

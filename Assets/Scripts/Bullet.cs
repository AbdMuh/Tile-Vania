using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float bulletSpeed;
    private GameObject _player; 
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Ray");
        bulletSpeed = 10f * _player.transform.localScale.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = new Vector2(bulletSpeed, 0f);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name);
    
        if (!other.gameObject.CompareTag("Enemy"))  Destroy(gameObject);;
    }
}

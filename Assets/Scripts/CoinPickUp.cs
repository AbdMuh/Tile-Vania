using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    bool hasPickedUp = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !hasPickedUp)
        {
            hasPickedUp = true;
            GameSession.Instance.AddToScore(10);
            Debug.Log("Playing effect");
            AudioManager.Instance.PlayEffect(0);
            Destroy(gameObject);
        }
    }
}
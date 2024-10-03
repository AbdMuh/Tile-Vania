using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelProgression : MonoBehaviour
{
    GameObject player; 
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            StartCoroutine(LoadLevel());
        }
    }
    private IEnumerator LoadLevel()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        yield return new WaitForSeconds(1.3f);
        int scene = SceneManager.GetActiveScene().buildIndex;
        scene++;
        if (scene == 3)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
    }
}

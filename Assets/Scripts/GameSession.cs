using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public int playerLives = 3;
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length; 
        // RETURNS LENGTH OF ARRAY
        if(numGameSessions > 1)
        {
            // IF THERE IS MORE THAN ONE GAMESESSION OBJECT
            // THEN DESTROY THE NEWLY CREATED GAMESESSION OBJECT
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public void TakeLife()
    {
        playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }
    private void ResetGameSession()
    {
       SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    
}

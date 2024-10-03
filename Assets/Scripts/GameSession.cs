using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public int playerLives = 3;
    public static GameSession Instance;
    void Awake()
    
    {
        // RETURNS LENGTH OF ARRAY
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // IF THERE IS MORE THAN ONE GAMESESSION OBJECT
            // THEN DESTROY THE NEWLY CREATED GAMESESSION OBJECT
            Destroy(gameObject);
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

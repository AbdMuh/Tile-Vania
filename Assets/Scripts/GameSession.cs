using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
	[SerializeField] public TextMeshProUGUI livesText;
	[SerializeField] public TextMeshProUGUI scoreText;
    public int score = 0;
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
	public void Start(){
	livesText.text = "LIVES:  "+playerLives.ToString();
    scoreText.text = "SCORE:  "+score.ToString();
	}
    
    public void TakeLife()
    {
        playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = "LIVES:  "+playerLives.ToString();
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
            ScenePersist.Instance.SceneReset();
        }
    }
    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
        scoreText.text = "SCORE:  "+score.ToString();
    }
    private void ResetGameSession()
    {
       SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    
}

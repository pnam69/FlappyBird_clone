using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    [Header("UI References")]
    public Text scoreText;
    public GameObject gameOverScreen;
    
    [Header("Score Settings")]
    public int playerScore = 0;
    
    private bool isGameOver = false;

    void Start()
    {
        UpdateScoreDisplay();
        
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }

    [ContextMenu("Add Score")]
    public void addScore(int scoreToAdd)
    {
        if (isGameOver) return;
        
        playerScore += scoreToAdd;
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = playerScore.ToString();
        }
    }

    public void restartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        if (isGameOver) return;
        
        isGameOver = true;
        
        // Play game over sound
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayGameOver();
        }
        
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}

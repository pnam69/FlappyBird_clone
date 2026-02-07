using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SimpleStartScreen : MonoBehaviour
{
    [Header("Simple Setup - Just assign your Score Text")]
    public Text scoreText;
    
    private bool gameStarted = false;
    private GameObject bird;
    private Rigidbody2D birdRb;

    void Start()
    {
        // Find the bird automatically
        bird = GameObject.FindGameObjectWithTag("Player");
        if (bird != null)
        {
            birdRb = bird.GetComponent<Rigidbody2D>();
            if (birdRb != null)
            {
                birdRb.gravityScale = 0; // Disable gravity
                birdRb.linearVelocity = Vector2.zero;
            }
        }
        
        // Hide score until game starts
        if (scoreText != null)
        {
            scoreText.enabled = false;
        }
        
        // Show instructions on score text position
        ShowStartMessage();
    }

    void Update()
    {
        if (!gameStarted)
        {
            // Check for keyboard input
            if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
            {
                StartGame();
            }
            
            // Check for mouse input
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                StartGame();
            }
            
            // Check for touch input (mobile)
            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                StartGame();
            }
        }
    }

    void ShowStartMessage()
    {
        if (scoreText != null)
        {
            scoreText.enabled = true;
            scoreText.text = "Press Any Key to Start";
            scoreText.fontSize = 30;
        }
    }

    void StartGame()
    {
        gameStarted = true;
        
        // Enable bird gravity
        if (birdRb != null)
        {
            birdRb.gravityScale = 2.7f; // Adjust to match your game
        }
        
        // Reset score display
        if (scoreText != null)
        {
            scoreText.text = "0";
            scoreText.fontSize = 50; // Back to normal size
        }
    }
}

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
    private PipeSpawner pipeSpawner;
    private PipeMove[] allPipes;

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
        
        // Find and disable pipe spawner
        pipeSpawner = FindObjectOfType<PipeSpawner>();
        if (pipeSpawner != null)
        {
            pipeSpawner.enabled = false;
        }
        
        // Find and disable all existing pipes
        allPipes = FindObjectsOfType<PipeMove>();
        foreach (PipeMove pipe in allPipes)
        {
            pipe.enabled = false;
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
            birdRb.gravityScale = 5f; // Adjust to match your game
        }
        
        // Enable pipe spawner
        if (pipeSpawner != null)
        {
            pipeSpawner.enabled = true;
        }
        
        // Enable all pipes
        foreach (PipeMove pipe in allPipes)
        {
            if (pipe != null)
            {
                pipe.enabled = true;
            }
        }
        
        // Reset score display
        if (scoreText != null)
        {
            scoreText.text = "0";
            scoreText.fontSize = 150; // Back to normal size
        }
    }
}

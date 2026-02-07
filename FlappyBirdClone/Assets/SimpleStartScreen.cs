using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class SimpleStartScreen : MonoBehaviour
{
    [Header("UI References")]
    public Text scoreText;
    public GameObject startScreenUI;
    
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
        pipeSpawner = Object.FindFirstObjectByType<PipeSpawner>();
        if (pipeSpawner != null)
        {
            pipeSpawner.enabled = false;
        }
        
        // Find and disable all existing pipes
        allPipes = Object.FindObjectsByType<PipeMove>(FindObjectsSortMode.None);
        foreach (PipeMove pipe in allPipes)
        {
            pipe.enabled = false;
        }
        
        // Hide score until game starts
        if (scoreText != null)
        {
            scoreText.enabled = false;
        }
        
        // Show start screen
        if (startScreenUI != null)
        {
            startScreenUI.SetActive(true);
        }
    }

    void Update()
    {
        if (!gameStarted)
        {
            // Check for keyboard input (keyboards don't interact with UI)
            if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
            {
                StartGame();
                return;
            }
            
            // Check for mouse input - only block if over UI
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                bool isOverUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
                if (!isOverUI)
                {
                    StartGame();
                }
                return;
            }
            
            // Check for touch input - only block if over UI
            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                bool isOverUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(0);
                if (!isOverUI)
                {
                    StartGame();
                }
            }
        }
    }

    void StartGame()
    {
        gameStarted = true;
        
        // Hide start screen
        if (startScreenUI != null)
        {
            startScreenUI.SetActive(false);
        }
        
        // Show score
        if (scoreText != null)
        {
            scoreText.enabled = true;
        }
        
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
    }
}


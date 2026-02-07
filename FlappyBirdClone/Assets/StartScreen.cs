using UnityEngine;

public class StartScreen : MonoBehaviour
{
    [Header("UI References")]
    public GameObject startScreenPanel;
    public GameObject gameplayUI;
    
    [Header("Bird Control")]
    public Rigidbody2D birdRigidbody;
    
    [Header("Settings")]
    public bool freezeGameOnStart = true;
    public bool disableBirdPhysics = true;
    
    private bool gameStarted = false;
    private LogicScript logic;
    private float originalGravityScale;

    void Start()
    {
        logic = FindObjectOfType<LogicScript>();
        
        // Save original bird settings
        if (birdRigidbody != null && disableBirdPhysics)
        {
            originalGravityScale = birdRigidbody.gravityScale;
            birdRigidbody.gravityScale = 0;
            birdRigidbody.linearVelocity = Vector2.zero;
        }
        
        // Show start screen
        if (startScreenPanel != null)
        {
            startScreenPanel.SetActive(true);
        }
        
        // Hide gameplay UI
        if (gameplayUI != null)
        {
            gameplayUI.SetActive(false);
        }
        
        // Freeze the game
        if (freezeGameOnStart)
        {
            Time.timeScale = 0f;
        }
    }

    void Update()
    {
        // Wait for any input to start
        if (!gameStarted)
        {
            // Keyboard/Mouse for PC
            if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
            {
                StartGame();
            }
            
            // Touch for mobile
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                StartGame();
            }
        }
    }

    public void StartGame()
    {
        if (gameStarted) return;
        
        gameStarted = true;
        
        // Enable bird physics
        if (birdRigidbody != null && disableBirdPhysics)
        {
            birdRigidbody.gravityScale = originalGravityScale;
        }
        
        // Hide start screen
        if (startScreenPanel != null)
        {
            startScreenPanel.SetActive(false);
        }
        
        // Show gameplay UI
        if (gameplayUI != null)
        {
            gameplayUI.SetActive(true);
        }
        
        // Unfreeze the game
        Time.timeScale = 1f;
        
        // Play start sound if available
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayFlap();
        }
    }
}

using UnityEngine;

public class BirdIdle : MonoBehaviour
{
    [Header("Bob Settings")]
    public float bobSpeed = 1.5f;
    public float bobAmount = 0.3f;
    
    private Vector3 startPosition;
    private bool isIdle = true;
    private StartScreen startScreen;
    
    void Start()
    {
        startPosition = transform.position;
        startScreen = FindObjectOfType<StartScreen>();
    }
    
    void Update()
    {
        if (!isIdle) return;
        
        // Use unscaledTime so it works even when game is frozen
        float newY = startPosition.y + Mathf.Sin(Time.unscaledTime * bobSpeed) * bobAmount;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
    
    public void StopIdle()
    {
        isIdle = false;
        transform.position = startPosition;
    }
    
    // Automatically stop idle when game starts
    void OnEnable()
    {
        if (startScreen != null)
        {
            // Subscribe to game start event if you add one
        }
    }
}

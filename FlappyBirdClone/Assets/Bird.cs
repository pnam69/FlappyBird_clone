using UnityEngine;
using UnityEngine.InputSystem;

public class Bird : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb2d;
    
    [Header("Flight Settings")]
    public float flapStrength = 12.0f;
    public float maxFallSpeed = 8f;
    public float tiltSpeed = 2f;
    public float maxTiltAngle = 40f;
    public float minTiltAngle = -40f;
    
    [Header("Bounds")]
    public float deathZoneY = -10f;
    public float ceilingY = 10f;
    
    private LogicScript logic;
    private bool isAlive = true;
    private bool hasGameOverTriggered = false;

    void Start()
    {
        GameObject logicObject = GameObject.FindGameObjectWithTag("Logic");
        if (logicObject != null)
        {
            logic = logicObject.GetComponent<LogicScript>();
        }
        else
        {
            Debug.LogError("Logic GameObject not found! Make sure there's a GameObject tagged 'Logic'.");
        }
    }

    void Update()
    {
        if (!isAlive) return;

        HandleInput();
        UpdateRotation();
        CheckBounds();
        ClampVelocity();
    }

    void HandleInput()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Flap();
        }
    }

    void Flap()
    {
        rb2d.linearVelocity = Vector2.up * flapStrength;
    }

    void UpdateRotation()
    {
        float velocity = rb2d.linearVelocity.y;
        float targetAngle = Mathf.Lerp(minTiltAngle, maxTiltAngle, (velocity + 10f) / 20f);
        targetAngle = Mathf.Clamp(targetAngle, minTiltAngle, maxTiltAngle);
        
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);
    }

    void ClampVelocity()
    {
        if (rb2d.linearVelocity.y < -maxFallSpeed)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, -maxFallSpeed);
        }
    }

    void CheckBounds()
    {
        if (transform.position.y < deathZoneY || transform.position.y > ceilingY)
        {
            Die();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }

    void Die()
    {
        if (hasGameOverTriggered) return;
        
        hasGameOverTriggered = true;
        isAlive = false;
        
        if (logic != null)
        {
            logic.gameOver();
        }
    }
}

using UnityEngine;

public class PipeMove : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10.0f;
    
    [Header("Cleanup")]
    public float deadZoneX = -35f;
    
    private LogicScript logic;

    void Start()
    {
        GameObject logicObject = GameObject.FindGameObjectWithTag("Logic");
        if (logicObject != null)
        {
            logic = logicObject.GetComponent<LogicScript>();
        }
    }

    void Update()
    {
        if (logic != null && logic.IsGameOver())
        {
            return;
        }
        
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        
        if (transform.position.x < deadZoneX)
        {
            Destroy(gameObject);
        }
    }
}

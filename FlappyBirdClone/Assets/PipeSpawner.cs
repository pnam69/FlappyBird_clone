using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject pipePrefab;
    public float spawnInterval = 2.0f;
    public float initialDelay = 0f;
    public float spawnOffsetX = 5.0f;
    
    [Header("Height Variation")]
    public float minHeight = -0.5f;
    public float maxHeight = 3f;
    
    private float timer = 0.0f;
    private LogicScript logic;
   //private bool hasStarted = false;

    void Start()
    {
        GameObject logicObject = GameObject.FindGameObjectWithTag("Logic");
        if (logicObject != null)
        {
            logic = logicObject.GetComponent<LogicScript>();
        }
        
        timer = -initialDelay;
    }

    void Update()
    {
        if (pipePrefab == null)
        {
            Debug.LogWarning("Pipe Prefab is not assigned in PipeSpawner!");
            return;
        }
        
        if (logic != null && logic.IsGameOver())
        {
            return;
        }
        
        timer += Time.deltaTime;
        
        if (timer >= spawnInterval)
        {
            SpawnPipe();
            timer = 0.0f;
        }
    }

    void SpawnPipe()
    {
        float randomHeight = Random.Range(minHeight, maxHeight);
        Vector3 spawnPosition = new Vector3(transform.position.x + spawnOffsetX, randomHeight, 0);
        Instantiate(pipePrefab, spawnPosition, Quaternion.identity);
    }
}

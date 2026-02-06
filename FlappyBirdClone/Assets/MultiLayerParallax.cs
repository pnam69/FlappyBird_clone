using UnityEngine;

public class MultiLayerParallax : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        [Tooltip("Parent GameObject containing all sprites for this layer")]
        public GameObject layerParent;
        
        [Tooltip("All GameObjects (sprites) in this layer - add duplicates here")]
        public GameObject[] layerObjects;
        
        public float scrollSpeed = 1.0f;
        
        [HideInInspector] public float layerWidth;
        [HideInInspector] public float resetPosition;
        [HideInInspector] public float despawnPosition;
    }
    
    [Header("Parallax Layers")]
    public ParallaxLayer[] layers;
    
    [Header("Settings")]
    public float globalSpeedMultiplier = 1.0f;
    
    [Header("Threshold Settings")]
    [Tooltip("Extra distance beyond camera view before despawning (negative)")]
    public float despawnBuffer = 5f;
    
    [Tooltip("Extra distance beyond rightmost sprite before respawning")]
    public float respawnBuffer = 0.5f;
    
    private LogicScript logic;
    private bool isGameOver = false;
    private Camera mainCamera;
    private float cameraLeftEdge;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found! MultiLayerParallax requires a camera tagged 'MainCamera'.");
        }
        
        GameObject logicObject = GameObject.FindGameObjectWithTag("Logic");
        if (logicObject != null)
        {
            logic = logicObject.GetComponent<LogicScript>();
        }
        
        InitializeLayers();
        CalculateThresholds();
    }

    void InitializeLayers()
    {
        foreach (ParallaxLayer layer in layers)
        {
            if (layer.layerObjects != null && layer.layerObjects.Length > 0)
            {
                GameObject firstObject = layer.layerObjects[0];
                if (firstObject != null)
                {
                    SpriteRenderer spriteRenderer = firstObject.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        layer.layerWidth = spriteRenderer.bounds.size.x;
                    }
                    else
                    {
                        Debug.LogWarning($"Layer object {firstObject.name} doesn't have a SpriteRenderer!");
                    }
                }
            }
        }
    }

    void CalculateThresholds()
    {
        if (mainCamera != null)
        {
            float cameraHeight = mainCamera.orthographicSize * 2f;
            float cameraWidth = cameraHeight * mainCamera.aspect;
            cameraLeftEdge = mainCamera.transform.position.x - (cameraWidth / 2f);
        }
        else
        {
            cameraLeftEdge = -10f;
        }
        
        foreach (ParallaxLayer layer in layers)
        {
            layer.despawnPosition = cameraLeftEdge - despawnBuffer;
            layer.resetPosition = respawnBuffer;
        }
    }

    void Update()
    {
        if (logic != null && logic.IsGameOver())
        {
            isGameOver = true;
            return;
        }
        
        if (isGameOver) return;
        
        foreach (ParallaxLayer layer in layers)
        {
            MoveLayer(layer);
        }
    }

    void MoveLayer(ParallaxLayer layer)
    {
        if (layer.layerObjects == null || layer.layerObjects.Length == 0) return;
        
        float speed = layer.scrollSpeed * globalSpeedMultiplier * Time.deltaTime;
        
        foreach (GameObject obj in layer.layerObjects)
        {
            if (obj == null) continue;
            
            obj.transform.position += Vector3.left * speed;
            
            // Check if sprite's right edge has passed the despawn threshold
            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
            float checkPosition = obj.transform.position.x;
            if (spriteRenderer != null)
            {
                checkPosition = obj.transform.position.x + (spriteRenderer.bounds.size.x / 2f);
            }
            
            if (checkPosition < layer.despawnPosition)
            {
                float rightmostX = GetRightmostXInLayer(layer);
                // Position based on transform position, not edge
                obj.transform.position = new Vector3(
                    rightmostX + layer.layerWidth + layer.resetPosition,
                    obj.transform.position.y,
                    obj.transform.position.z
                );
            }
        }
    }

    float GetRightmostXInLayer(ParallaxLayer layer)
    {
        float rightmostX = float.MinValue;
        
        foreach (GameObject obj in layer.layerObjects)
        {
            if (obj == null) continue;
            
            // Use transform position for tighter packing
            if (obj.transform.position.x > rightmostX)
            {
                rightmostX = obj.transform.position.x;
            }
        }
        
        return rightmostX;
    }
}

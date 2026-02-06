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
    }
    
    [Header("Parallax Layers")]
    public ParallaxLayer[] layers;
    
    [Header("Settings")]
    public float globalSpeedMultiplier = 1.0f;
    
    [Header("Respawn Settings")]
    [Tooltip("How far off screen (left) before respawning")]
    public float despawnOffset = 2f;
    
    private LogicScript logic;
    private bool isGameOver = false;
    private Camera mainCamera;
    private float cameraLeftEdge;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found!");
        }
        
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
            isGameOver = true;
            return;
        }
        
        if (isGameOver) return;
        
        UpdateCameraEdge();
        
        foreach (ParallaxLayer layer in layers)
        {
            MoveLayer(layer);
        }
    }

    void UpdateCameraEdge()
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
    }

    void MoveLayer(ParallaxLayer layer)
    {
        if (layer.layerObjects == null || layer.layerObjects.Length == 0) return;
        
        float speed = layer.scrollSpeed * globalSpeedMultiplier * Time.deltaTime;
        
        foreach (GameObject obj in layer.layerObjects)
        {
            if (obj == null) continue;
            
            // Move sprite
            obj.transform.position += Vector3.left * speed;
            
            // Get sprite's right edge
            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
            float spriteRightEdge = obj.transform.position.x;
            
            if (spriteRenderer != null)
            {
                spriteRightEdge = obj.transform.position.x + (spriteRenderer.bounds.size.x / 2f);
            }
            
            // If sprite's right edge has passed the left edge of camera, respawn it
            if (spriteRightEdge < cameraLeftEdge - despawnOffset)
            {
                // Find the rightmost sprite in this layer
                float rightmostEdge = GetRightmostEdgeInLayer(layer);
                
                // Get this sprite's width
                float spriteWidth = spriteRenderer != null ? spriteRenderer.bounds.size.x : 20f;
                
                // Position it right after the rightmost sprite
                obj.transform.position = new Vector3(
                    rightmostEdge + (spriteWidth / 2f),
                    obj.transform.position.y,
                    obj.transform.position.z
                );
            }
        }
    }

    float GetRightmostEdgeInLayer(ParallaxLayer layer)
    {
        float rightmostEdge = float.MinValue;
        
        foreach (GameObject obj in layer.layerObjects)
        {
            if (obj == null) continue;
            
            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
            float objRightEdge = obj.transform.position.x;
            
            if (spriteRenderer != null)
            {
                objRightEdge = obj.transform.position.x + (spriteRenderer.bounds.size.x / 2f);
            }
            
            if (objRightEdge > rightmostEdge)
            {
                rightmostEdge = objRightEdge;
            }
        }
        
        return rightmostEdge;
    }
}

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
    [Tooltip("Respawn when sprite is this far left")]
    public float despawnX = -26f;
    
    [Tooltip("Gap between sprites when respawning (negative for overlap)")]
    public float respawnGap = 0f;
    
    private LogicScript logic;
    private bool isGameOver = false;

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
            
            // Move sprite
            obj.transform.position += Vector3.left * speed;
            
            // If sprite went too far left, respawn it to the right
            if (obj.transform.position.x < despawnX)
            {
                // Find the rightmost sprite in this layer
                float rightmostX = GetRightmostXInLayer(layer);
                
                // Get sprite width
                SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
                float spriteWidth = spriteRenderer != null ? spriteRenderer.bounds.size.x : 20f;
                
                // Position it right after the rightmost sprite
                // rightmostX is the center of rightmost sprite
                // Add half of rightmost width + half of this sprite width + gap
                obj.transform.position = new Vector3(
                    rightmostX + spriteWidth + respawnGap,
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
            
            if (obj.transform.position.x > rightmostX)
            {
                rightmostX = obj.transform.position.x;
            }
        }
        
        return rightmostX;
    }
}

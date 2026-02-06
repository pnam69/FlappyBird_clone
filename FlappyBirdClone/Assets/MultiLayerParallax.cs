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
    }
    
    [Header("Parallax Layers")]
    public ParallaxLayer[] layers;
    
    [Header("Settings")]
    public float globalSpeedMultiplier = 1.0f;
    
    private LogicScript logic;
    private bool isGameOver = false;

    void Start()
    {
        GameObject logicObject = GameObject.FindGameObjectWithTag("Logic");
        if (logicObject != null)
        {
            logic = logicObject.GetComponent<LogicScript>();
        }
        
        InitializeLayers();
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
            
            // Simple check: if object has moved one full width to the left, teleport it to the right
            float rightmostX = GetRightmostXInLayer(layer, obj);
            
            if (obj.transform.position.x < rightmostX - layer.layerWidth)
            {
                obj.transform.position = new Vector3(
                    rightmostX + layer.layerWidth,
                    obj.transform.position.y,
                    obj.transform.position.z
                );
            }
        }
    }

    float GetRightmostXInLayer(ParallaxLayer layer, GameObject excludeObject)
    {
        float rightmostX = float.MinValue;
        
        foreach (GameObject obj in layer.layerObjects)
        {
            if (obj == null || obj == excludeObject) continue;
            
            if (obj.transform.position.x > rightmostX)
            {
                rightmostX = obj.transform.position.x;
            }
        }
        
        return rightmostX;
    }
}

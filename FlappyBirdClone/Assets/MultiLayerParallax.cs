using UnityEngine;

public class MultiLayerParallax : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public GameObject layerObject;
        public float scrollSpeed = 1.0f;
        [HideInInspector] public Vector3 startPosition;
        [HideInInspector] public float layerWidth;
    }
    
    [Header("Parallax Layers")]
    public ParallaxLayer[] layers;
    
    [Header("Settings")]
    public bool useGameSpeed = true;
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
            if (layer.layerObject != null)
            {
                layer.startPosition = layer.layerObject.transform.position;
                
                SpriteRenderer spriteRenderer = layer.layerObject.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    layer.layerWidth = spriteRenderer.bounds.size.x;
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
            if (layer.layerObject != null)
            {
                MoveLayer(layer);
            }
        }
    }

    void MoveLayer(ParallaxLayer layer)
    {
        float speed = layer.scrollSpeed * globalSpeedMultiplier * Time.deltaTime;
        layer.layerObject.transform.position += Vector3.left * speed;
        
        if (layer.layerObject.transform.position.x <= layer.startPosition.x - layer.layerWidth)
        {
            layer.layerObject.transform.position = new Vector3(
                layer.startPosition.x,
                layer.layerObject.transform.position.y,
                layer.layerObject.transform.position.z
            );
        }
    }
}

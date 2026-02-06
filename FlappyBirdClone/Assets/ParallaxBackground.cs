using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Header("Parallax Settings")]
    public float parallaxSpeed = 0.5f;
    public bool infiniteScroll = true;
    
    [Header("References")]
    private Material material;
    private LogicScript logic;
    private float offset = 0f;

    void Start()
    {
        GameObject logicObject = GameObject.FindGameObjectWithTag("Logic");
        if (logicObject != null)
        {
            logic = logicObject.GetComponent<LogicScript>();
        }
        
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
        }
        else
        {
            Debug.LogWarning("ParallaxBackground requires a Renderer component!");
        }
    }

    void Update()
    {
        if (material == null) return;
        
        if (logic != null && logic.IsGameOver())
        {
            return;
        }
        
        if (infiniteScroll)
        {
            offset += parallaxSpeed * Time.deltaTime;
            material.mainTextureOffset = new Vector2(offset, 0);
        }
    }
}

using UnityEngine;

public class ParallaxDebug : MonoBehaviour
{
    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 16;
        style.normal.textColor = Color.yellow;
        
        MultiLayerParallax parallax = GetComponent<MultiLayerParallax>();
        if (parallax == null)
        {
            GUI.Label(new Rect(10, 10, 500, 30), "MultiLayerParallax script not found!", style);
            return;
        }
        
        int yOffset = 10;
        GUI.Label(new Rect(10, yOffset, 500, 30), "=== PARALLAX DEBUG ===", style);
        yOffset += 25;
        
        if (parallax.layers == null || parallax.layers.Length == 0)
        {
            GUI.Label(new Rect(10, yOffset, 500, 30), "No layers configured!", style);
            return;
        }
        
        GUI.Label(new Rect(10, yOffset, 500, 30), $"Total Layers: {parallax.layers.Length}", style);
        yOffset += 25;
        
        for (int i = 0; i < parallax.layers.Length; i++)
        {
            var layer = parallax.layers[i];
            GUI.Label(new Rect(10, yOffset, 500, 30), $"Layer {i}:", style);
            yOffset += 20;
            
            if (layer.layerObjects == null || layer.layerObjects.Length == 0)
            {
                GUI.Label(new Rect(30, yOffset, 500, 30), "  No objects in layer!", style);
                yOffset += 20;
                continue;
            }
            
            GUI.Label(new Rect(30, yOffset, 500, 30), $"  Objects: {layer.layerObjects.Length}, Speed: {layer.scrollSpeed}", style);
            yOffset += 20;
            
            for (int j = 0; j < layer.layerObjects.Length; j++)
            {
                var obj = layer.layerObjects[j];
                if (obj == null)
                {
                    GUI.Label(new Rect(50, yOffset, 500, 30), $"  [{j}] NULL!", style);
                }
                else
                {
                    Vector3 pos = obj.transform.position;
                    SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                    string spriteInfo = sr != null ? $"Sprite: {sr.sprite?.name ?? "null"}" : "No SpriteRenderer";
                    GUI.Label(new Rect(50, yOffset, 700, 30), $"  [{j}] {obj.name} at ({pos.x:F1}, {pos.y:F1}, {pos.z:F1}) - {spriteInfo}", style);
                }
                yOffset += 20;
            }
        }
        
        Camera cam = Camera.main;
        if (cam != null)
        {
            yOffset += 10;
            GUI.Label(new Rect(10, yOffset, 500, 30), $"Camera: ({cam.transform.position.x:F1}, {cam.transform.position.y:F1}, {cam.transform.position.z:F1})", style);
        }
    }
}

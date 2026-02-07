using UnityEngine;
using TMPro;

public class PulseText : MonoBehaviour
{
    [Header("Pulse Settings")]
    public float pulseSpeed = 2f;
    public float minAlpha = 0.3f;
    public float maxAlpha = 1f;
    
    private TextMeshProUGUI tmpText;
    private UnityEngine.UI.Text legacyText;
    private bool useTMP;
    
    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        if (tmpText != null)
        {
            useTMP = true;
        }
        else
        {
            legacyText = GetComponent<UnityEngine.UI.Text>();
        }
    }
    
    void Update()
    {
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, 
            (Mathf.Sin(Time.unscaledTime * pulseSpeed) + 1f) / 2f);
        
        if (useTMP && tmpText != null)
        {
            Color color = tmpText.color;
            color.a = alpha;
            tmpText.color = color;
        }
        else if (legacyText != null)
        {
            Color color = legacyText.color;
            color.a = alpha;
            legacyText.color = color;
        }
    }
}

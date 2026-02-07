using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [Header("UI References")]
    public GameObject settingsPanel;
    public Slider sfxSlider;
    public Slider musicSlider;
    public Button closeButton;
    
    [Header("Optional - Volume Text Display")]
    public Text sfxVolumeText;
    public Text musicVolumeText;
    
    private bool isOpen = false;

    void Start()
    {
        // Hide settings panel on start
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
        
        // Setup sliders if AudioManager exists
        if (AudioManager.Instance != null)
        {
            if (sfxSlider != null)
            {
                sfxSlider.value = AudioManager.Instance.sfxVolume;
                sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
            }
            
            if (musicSlider != null)
            {
                musicSlider.value = AudioManager.Instance.musicVolume;
                musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            }
        }
        
        // Setup close button
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseSettings);
        }
        
        // Update text displays
        UpdateVolumeText();
    }

    void Update()
    {
        // Toggle settings with Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettings();
        }
    }

    public void ToggleSettings()
    {
        if (isOpen)
        {
            CloseSettings();
        }
        else
        {
            OpenSettings();
        }
    }

    public void OpenSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
            isOpen = true;
            
            // Optionally pause game when settings open
            // Time.timeScale = 0f;
        }
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
            isOpen = false;
            
            // Unpause if you paused
            // Time.timeScale = 1f;
        }
    }

    void OnSFXVolumeChanged(float value)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.sfxVolume = value;
            UpdateVolumeText();
            
            // Play a test sound
            AudioManager.Instance.PlayFlap();
        }
    }

    void OnMusicVolumeChanged(float value)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.musicVolume = value;
            
            // Update the actual music volume immediately
            if (AudioManager.Instance.musicSource != null)
            {
                AudioManager.Instance.musicSource.volume = value;
            }
            
            UpdateVolumeText();
        }
    }

    void UpdateVolumeText()
    {
        if (AudioManager.Instance == null) return;
        
        if (sfxVolumeText != null)
        {
            sfxVolumeText.text = Mathf.RoundToInt(AudioManager.Instance.sfxVolume * 100) + "%";
        }
        
        if (musicVolumeText != null)
        {
            musicVolumeText.text = Mathf.RoundToInt(AudioManager.Instance.musicVolume * 100) + "%";
        }
    }
}

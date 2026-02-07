using UnityEngine;

public class QuickVolumeControl : MonoBehaviour
{
    [Header("Keyboard Shortcuts")]
    public KeyCode increaseSFXKey = KeyCode.Plus;
    public KeyCode decreaseSFXKey = KeyCode.Minus;
    public KeyCode increaseMusicKey = KeyCode.RightBracket;
    public KeyCode decreaseMusicKey = KeyCode.LeftBracket;
    public KeyCode muteSFXKey = KeyCode.M;
    public KeyCode muteMusicKey = KeyCode.N;
    
    [Header("Volume Step")]
    public float volumeStep = 0.1f;
    
    private bool sfxMuted = false;
    private bool musicMuted = false;
    private float lastSFXVolume = 0.7f;
    private float lastMusicVolume = 0.5f;

    void Update()
    {
        if (AudioManager.Instance == null) return;
        
        // SFX Volume Controls
        if (Input.GetKeyDown(increaseSFXKey))
        {
            ChangeSFXVolume(volumeStep);
        }
        if (Input.GetKeyDown(decreaseSFXKey))
        {
            ChangeSFXVolume(-volumeStep);
        }
        if (Input.GetKeyDown(muteSFXKey))
        {
            ToggleSFXMute();
        }
        
        // Music Volume Controls
        if (Input.GetKeyDown(increaseMusicKey))
        {
            ChangeMusicVolume(volumeStep);
        }
        if (Input.GetKeyDown(decreaseMusicKey))
        {
            ChangeMusicVolume(-volumeStep);
        }
        if (Input.GetKeyDown(muteMusicKey))
        {
            ToggleMusicMute();
        }
    }

    void ChangeSFXVolume(float change)
    {
        AudioManager.Instance.sfxVolume = Mathf.Clamp01(AudioManager.Instance.sfxVolume + change);
        sfxMuted = false;
        Debug.Log("SFX Volume: " + Mathf.RoundToInt(AudioManager.Instance.sfxVolume * 100) + "%");
        
        // Play test sound
        AudioManager.Instance.PlayFlap();
    }

    void ChangeMusicVolume(float change)
    {
        AudioManager.Instance.musicVolume = Mathf.Clamp01(AudioManager.Instance.musicVolume + change);
        
        if (AudioManager.Instance.musicSource != null)
        {
            AudioManager.Instance.musicSource.volume = AudioManager.Instance.musicVolume;
        }
        
        musicMuted = false;
        Debug.Log("Music Volume: " + Mathf.RoundToInt(AudioManager.Instance.musicVolume * 100) + "%");
    }

    void ToggleSFXMute()
    {
        if (sfxMuted)
        {
            AudioManager.Instance.sfxVolume = lastSFXVolume;
            sfxMuted = false;
            Debug.Log("SFX Unmuted");
        }
        else
        {
            lastSFXVolume = AudioManager.Instance.sfxVolume;
            AudioManager.Instance.sfxVolume = 0;
            sfxMuted = true;
            Debug.Log("SFX Muted");
        }
    }

    void ToggleMusicMute()
    {
        if (musicMuted)
        {
            AudioManager.Instance.musicVolume = lastMusicVolume;
            if (AudioManager.Instance.musicSource != null)
            {
                AudioManager.Instance.musicSource.volume = lastMusicVolume;
            }
            musicMuted = false;
            Debug.Log("Music Unmuted");
        }
        else
        {
            lastMusicVolume = AudioManager.Instance.musicVolume;
            AudioManager.Instance.musicVolume = 0;
            if (AudioManager.Instance.musicSource != null)
            {
                AudioManager.Instance.musicSource.volume = 0;
            }
            musicMuted = true;
            Debug.Log("Music Muted");
        }
    }
}

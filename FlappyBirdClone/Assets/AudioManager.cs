using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [Header("Audio Sources")]
    [Tooltip("For sound effects (flap, hit, score)")]
    public AudioSource sfxSource;
    
    [Tooltip("For background music")]
    public AudioSource musicSource;
    
    [Header("Sound Effects")]
    public AudioClip flapSound;
    public AudioClip hitSound;
    public AudioClip scoreSound;
    public AudioClip gameOverSound;
    
    [Header("Music")]
    public AudioClip backgroundMusic;
    
    [Header("Volume Settings")]
    [Range(0f, 1f)]
    public float sfxVolume = 0.7f;
    
    [Range(0f, 1f)]
    public float musicVolume = 0.5f;
    
    [Header("Settings")]
    public bool playMusicOnStart = true;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        SetupAudioSources();
    }

    void Start()
    {
        if (playMusicOnStart && backgroundMusic != null)
        {
            PlayMusic(backgroundMusic);
        }
    }

    void SetupAudioSources()
    {
        // Create SFX source if not assigned
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }
        sfxSource.playOnAwake = false;
        sfxSource.volume = sfxVolume;
        
        // Create Music source if not assigned
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
        }
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.volume = musicVolume;
    }

    void Update()
    {
        // Update volumes in real-time
        if (sfxSource != null)
        {
            sfxSource.volume = sfxVolume;
        }
        
        if (musicSource != null)
        {
            musicSource.volume = musicVolume;
        }
    }

    // Play sound effect
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    // Play music
    public void PlayMusic(AudioClip clip)
    {
        if (clip != null && musicSource != null)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    // Stop music
    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    // Convenience methods
    public void PlayFlap()
    {
        PlaySFX(flapSound);
    }

    public void PlayHit()
    {
        PlaySFX(hitSound);
    }

    public void PlayScore()
    {
        PlaySFX(scoreSound);
    }

    public void PlayGameOver()
    {
        PlaySFX(gameOverSound);
    }
}

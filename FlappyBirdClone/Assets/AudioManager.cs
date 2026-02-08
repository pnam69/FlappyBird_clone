using UnityEngine;
using System.Collections;

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
    public float musicFadeDuration = 1.5f;
    
    private float targetMusicVolume;
    private Coroutine fadeCoroutine;

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
        targetMusicVolume = musicVolume;
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
        // Update SFX volume in real-time
        if (sfxSource != null)
        {
            sfxSource.volume = sfxVolume;
        }
        
        // Update target music volume (actual fading handled by coroutine)
        targetMusicVolume = musicVolume;
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
            
            // Fade in music
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(FadeMusicTo(musicVolume, musicFadeDuration));
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

    // Fade out music (for game over)
    public void FadeOutMusic()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeMusicTo(0f, musicFadeDuration));
    }

    // Fade in music (for restart/new game)
    public void FadeInMusic()
    {
        if (musicSource != null && !musicSource.isPlaying && backgroundMusic != null)
        {
            musicSource.Play();
        }
        
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeMusicTo(musicVolume, musicFadeDuration));
    }

    // Coroutine to fade music volume
    private IEnumerator FadeMusicTo(float targetVolume, float duration)
    {
        if (musicSource == null) yield break;
        
        float startVolume = musicSource.volume;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            musicSource.volume = Mathf.Lerp(startVolume, targetVolume, t);
            yield return null;
        }
        
        musicSource.volume = targetVolume;
        
        // Stop music completely if faded to 0
        if (targetVolume <= 0.01f && musicSource.isPlaying)
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
        FadeOutMusic();
    }
}

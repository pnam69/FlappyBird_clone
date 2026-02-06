# Audio System Setup Guide

## 🎵 What I've Created

A complete audio system with:
- **AudioManager** - Centralized sound management
- **Sound Effects** - Flap, hit, score, game over
- **Background Music** - Looping music support
- **Volume Controls** - Separate SFX and music volumes
- **Easy Integration** - Already integrated into all your scripts

---

## 🎮 Quick Setup (5 Minutes)

### Step 1: Create AudioManager GameObject

1. **In Unity Hierarchy**:
   - Right-click → Create Empty
   - Name it: **"AudioManager"**

2. **Add AudioManager Script**:
   - Select AudioManager GameObject
   - Add Component → Search "AudioManager"
   - The script will auto-create 2 AudioSource components

### Step 2: Add Sound Files

1. **Get Sound Effects** (see "Where to Get Sounds" below)
   
2. **Import to Unity**:
   - Drag your sound files into the **Assets** folder
   - Unity will automatically import them as AudioClips

3. **Assign Sounds to AudioManager**:
   - Select **AudioManager** GameObject
   - In Inspector, find the AudioManager component
   - Drag sound files to these slots:
     - **Flap Sound**: Wing flap/whoosh sound
     - **Hit Sound**: Collision/thud sound
     - **Score Sound**: Ding/point sound
     - **Game Over Sound**: Sad/losing sound
     - **Background Music**: (Optional) looping music

### Step 3: Adjust Settings

```
AudioManager Settings:
├── SFX Volume: 0.7 (adjust to taste)
├── Music Volume: 0.5 (lower for background)
└── Play Music On Start: ✓ (check if you have music)
```

**That's it!** Your game now has sound! 🎉

---

## 🎵 Where to Get Free Sounds

### Best Free Resources:

1. **Freesound.org**
   - Search: "flap", "whoosh", "hit", "ding", "game over"
   - Filter: "0 to 2 seconds" for SFX
   - Download WAV or OGG format

2. **OpenGameArt.org**
   - Category: Sound Effects
   - Search: "flappy bird", "casual game sounds"

3. **Kenney.nl**
   - Go to: kenney.nl/assets
   - Download: "Interface Sounds" or "Digital Audio" pack
   - 100% free, no attribution required

4. **Zapsplat.com**
   - Free with account
   - Professional quality sounds

5. **YouTube Audio Library**
   - Free sound effects and music
   - No copyright issues

### Recommended Searches:

- **Flap Sound**: "wing flap", "whoosh", "swish"
- **Hit Sound**: "thud", "bump", "collision"
- **Score Sound**: "ding", "coin", "point", "UI click"
- **Game Over Sound**: "fail", "lose", "game over jingle"
- **Background Music**: "8-bit loop", "casual game music"

---

## 🛠️ Advanced Features

### Change Volume at Runtime

The AudioManager has public sliders in the Inspector:
- Adjust **SFX Volume** (0-1)
- Adjust **Music Volume** (0-1)
- Changes apply in real-time, even during gameplay

### Play Custom Sounds

If you want to play additional sounds from other scripts:

```csharp
// Play any sound effect
AudioManager.Instance.PlaySFX(myCustomClip);

// Use convenience methods
AudioManager.Instance.PlayFlap();
AudioManager.Instance.PlayHit();
AudioManager.Instance.PlayScore();
AudioManager.Instance.PlayGameOver();

// Control music
AudioManager.Instance.PlayMusic(myMusicClip);
AudioManager.Instance.StopMusic();
```

### Mute/Unmute Toggle

Add these methods to AudioManager if you want mute buttons:

```csharp
public void ToggleSFX()
{
    sfxVolume = sfxVolume > 0 ? 0 : 0.7f;
}

public void ToggleMusic()
{
    musicVolume = musicVolume > 0 ? 0 : 0.5f;
}
```

---

## 📋 What's Already Integrated

I've already added sound calls to:

✅ **Bird.cs**
- `PlayFlap()` when you press space

✅ **Bird.cs (Die method)**
- `PlayHit()` when bird collides

✅ **TriggerScript.cs**
- `PlayScore()` when passing through pipes

✅ **LogicScript.cs**
- `PlayGameOver()` when game ends

**You don't need to code anything!** Just assign the sound files and it works!

---

## 🎯 Recommended Sound Specifications

For best performance in Unity:

- **Format**: WAV or OGG
- **Sample Rate**: 44100 Hz
- **Bit Depth**: 16-bit
- **Channels**: Mono (for SFX), Stereo (for music)
- **Length**: 
  - SFX: 0.1 - 1.0 seconds
  - Music: Any length (will loop automatically)

---

## 🔧 Troubleshooting

### No sound playing:
1. Check that AudioManager GameObject exists in scene
2. Check that sound clips are assigned in Inspector
3. Check that volumes are not 0
4. Check Unity Editor: Game view must have audio icon enabled (not muted)

### Sound plays but is very quiet:
- Increase SFX Volume or Music Volume in AudioManager
- Check AudioSource → Volume in Inspector

### Multiple sounds playing at once:
- This is normal! `PlayOneShot()` allows multiple sounds
- If you want only one, use `Play()` instead of `PlayOneShot()`

### Music doesn't loop:
- Check that Music Source → Loop is enabled (it should be automatic)
- Make sure you assigned the clip to Background Music slot

---

## 💡 Pro Tips

1. **Keep SFX short** (< 1 second) for responsive gameplay
2. **Test volume balance** - SFX should be louder than music
3. **Use mono for SFX** - saves memory and sounds better in 2D
4. **Normalize audio** - make sure all sounds have similar volume levels
5. **Compression** - Use OGG for music (smaller file size), WAV for SFX (faster)

---

## 🎨 Optional: Create a Simple UI

If you want mute buttons:

1. Create UI Buttons in Canvas
2. Name them "Mute SFX" and "Mute Music"
3. Add this script to AudioManager:

```csharp
public void ToggleSFX()
{
    sfxVolume = sfxVolume > 0 ? 0 : 0.7f;
}

public void ToggleMusic()
{
    musicVolume = musicVolume > 0 ? 0 : 0.5f;
}
```

4. Assign these methods to button OnClick events

---

## 📦 Quick Start Without Custom Sounds

If you just want to test the system:

1. Create AudioManager GameObject
2. Add AudioManager script
3. Leave all sound slots empty for now
4. Press Play - the game works without sounds (no errors)
5. Add sounds later when you find good ones

The system is **fully optional** - it won't break if sounds are missing!

---

## ✨ Summary

**What you get:**
- ✅ Professional audio system
- ✅ Easy to use (just drag & drop sounds)
- ✅ Volume controls
- ✅ Already integrated everywhere
- ✅ No coding required
- ✅ Works without sounds (optional)

**What you need:**
- 4 sound effect files (flap, hit, score, game over)
- 1 music file (optional)
- 5 minutes to set up

Enjoy your game with sound! 🎵🎮

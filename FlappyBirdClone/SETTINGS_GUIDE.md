# Settings Panel Setup - 3 Minute Guide

## 🎚️ Quick Setup

### Step 1: Create Settings Panel (1 minute)

1. **Create Panel:**
   ```
   Right-click Canvas → UI → Panel
   Name it: "SettingsPanel"
   ```

2. **Style Panel:**
   - Image → Color: Semi-transparent dark (RGBA: 0, 0, 0, 200)
   - Position: Center of screen
   - Size: About 400x300

3. **Add Title:**
   ```
   Right-click SettingsPanel → UI → Text
   Name: "TitleText"
   Text: "Settings"
   Font Size: 40-50
   Position: Top of panel
   ```

### Step 2: Add Volume Sliders (1 minute)

1. **SFX Volume:**
   ```
   Right-click SettingsPanel → UI → Slider
   Name: "SFXSlider"
   Position: Upper middle of panel
   
   Add Text above slider:
   - Name: "SFXLabel"
   - Text: "Sound Effects"
   
   Optional - Add Text beside slider:
   - Name: "SFXVolumeText"
   - Text: "70%"
   ```

2. **Music Volume:**
   ```
   Right-click SettingsPanel → UI → Slider
   Name: "MusicSlider"
   Position: Below SFX slider
   
   Add Text above slider:
   - Name: "MusicLabel"
   - Text: "Music"
   
   Optional - Add Text beside slider:
   - Name: "MusicVolumeText"
   - Text: "50%"
   ```

### Step 3: Add Close Button (30 seconds)

```
Right-click SettingsPanel → UI → Button
Name: "CloseButton"
Position: Bottom of panel
Button Text: "Close" or "X"
```

### Step 4: Add Settings Script (30 seconds)

1. **Create GameObject:**
   ```
   Right-click Hierarchy → Create Empty
   Name: "SettingsManager"
   ```

2. **Add Script:**
   ```
   Add Component → SettingsPanel
   ```

3. **Assign References:**
   ```
   Settings Panel: SettingsPanel
   SFX Slider: SFXSlider
   Music Slider: MusicSlider
   Close Button: CloseButton
   SFX Volume Text: SFXVolumeText (optional)
   Music Volume Text: MusicVolumeText (optional)
   ```

### Step 5: Add Settings Button (30 seconds)

**To your Start Screen or Game UI:**
```
Right-click StartScreenUI → UI → Button
Name: "SettingsButton"
Text: "⚙️" or "Settings"
Position: Top-right corner

Button OnClick:
- SettingsManager → SettingsPanel → OpenSettings()
```

**Done!** 🎉

---

## 🎮 Usage

- **Click Settings Button** → Opens settings
- **Adjust Sliders** → Changes volume in real-time
- **Click Close** → Closes settings
- **Press ESC** → Toggle settings on/off

---

## 📋 Visual Layout Example

```
┌────────────────────────────┐
│        Settings            │
│                            │
│  Sound Effects             │
│  ━━━━━━●━━━━━━━  70%       │
│                            │
│  Music                     │
│  ━━━━━●━━━━━━━━  50%       │
│                            │
│      [Close Button]        │
└────────────────────────────┘
```

---

## ⚙️ Features

- ✅ Real-time volume adjustment
- ✅ SFX test on slider change (plays flap sound)
- ✅ Percentage display (optional)
- ✅ ESC key to toggle
- ✅ Clean, simple UI

---

## 🎨 Optional Enhancements

### 1. Pause Game When Settings Open

Uncomment these lines in `SettingsPanel.cs`:

```csharp
public void OpenSettings()
{
    settingsPanel.SetActive(true);
    isOpen = true;
    Time.timeScale = 0f; // ✅ Uncomment
}

public void CloseSettings()
{
    settingsPanel.SetActive(false);
    isOpen = false;
    Time.timeScale = 1f; // ✅ Uncomment
}
```

### 2. Save Volume Settings

Add to `OnSFXVolumeChanged` and `OnMusicVolumeChanged`:

```csharp
PlayerPrefs.SetFloat("SFXVolume", value);
PlayerPrefs.Save();
```

Load in `Start`:
```csharp
float savedSFX = PlayerPrefs.GetFloat("SFXVolume", 0.7f);
sfxSlider.value = savedSFX;
```

### 3. Mute Buttons

Add toggle buttons:
```csharp
public void ToggleSFXMute()
{
    AudioManager.Instance.sfxVolume = 
        AudioManager.Instance.sfxVolume > 0 ? 0 : 0.7f;
    sfxSlider.value = AudioManager.Instance.sfxVolume;
}
```

---

## 🔧 Troubleshooting

### Sliders don't change volume:
- Check that AudioManager exists in scene
- Verify sliders are assigned in SettingsPanel component

### Settings panel doesn't open:
- Check that panel is assigned
- Make sure button OnClick is set up correctly

### Volume resets on restart:
- Add PlayerPrefs save/load (see Optional Enhancements)

---

## ✅ Quick Checklist

```
□ Created SettingsPanel
□ Added title text
□ Added SFX slider
□ Added Music slider
□ Added close button
□ Created SettingsManager GameObject
□ Added SettingsPanel script
□ Assigned all references
□ Added settings button to UI
□ Tested - sliders work!
```

---

## 🎯 Minimal Version (30 seconds)

If you want the absolute simplest version:

1. Create Panel with 2 sliders
2. Add SettingsPanel script to any GameObject
3. Assign just the sliders
4. Done - use ESC to toggle

The script handles everything automatically!

---

## 💡 Pro Tips

1. **Position sliders vertically** for easier use
2. **Use icons** (🔊 🎵) instead of text labels
3. **Test volumes** - make sure range feels good
4. **Add visual feedback** - slider handle color changes
5. **Keep it simple** - don't overwhelm with options

Your game now has volume control! 🎚️🎉

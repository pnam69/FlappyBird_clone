# Parallax Background Implementation Guide

## 📦 What I've Created

I've added two parallax scripts to your project:

1. **ParallaxBackground.cs** - Simple single-layer parallax (texture scrolling)
2. **MultiLayerParallax.cs** - Advanced multi-layer parallax (multiple GameObjects)

## 🎨 Method 1: Simple Texture Scrolling (Quick & Easy)

### Setup Steps:

1. **Create a Background Sprite**
   - In Unity, right-click in Hierarchy → `2D Object` → `Sprite`
   - Name it "Background"
   - Position: X=0, Y=0, Z=10 (behind everything)
   - Scale it to fill the camera view

2. **Assign a Texture**
   - Drag a background image to your Assets folder
   - Set Texture Type to `Sprite (2D and UI)`
   - Set Wrap Mode to `Repeat` (Important!)
   - Assign it to your Background sprite

3. **Add the Script**
   - Select the Background object
   - Add Component → `ParallaxBackground`
   - Set `Parallax Speed` to `0.1` - `0.3` (experiment!)

4. **Adjust Material**
   - Select your background sprite in Assets
   - In Inspector, change Material → Shader to `Sprites/Default`
   - Make sure Wrap Mode is `Repeat`

### Settings:
- **Parallax Speed**: `0.1-0.5` (slower = further away, faster = closer)
- **Infinite Scroll**: Keep checked for continuous scrolling

---

## 🏔️ Method 2: Multi-Layer Parallax (Professional Look)

### Setup Steps:

1. **Create Multiple Background Layers**
   - Create 2-3 Sprite GameObjects:
     - "Background_Sky" (furthest, slowest)
     - "Background_Mountains" (middle layer)
     - "Background_Ground" (closest, fastest)
   
2. **Position Layers**
   - Sky: X=0, Y=0, Z=10
   - Mountains: X=0, Y=-2, Z=9
   - Ground: X=0, Y=-4, Z=8
   - Scale each to fill camera width

3. **Duplicate for Seamless Loop**
   - Duplicate each layer (Ctrl+D)
   - Position duplicate EXACTLY to the right of original
   - Example: If sprite width is 20 units, place at X=20

4. **Add Multi-Layer Script**
   - Create an Empty GameObject: "ParallaxManager"
   - Add Component → `MultiLayerParallax`
   - Set Layers array size to 3

5. **Configure Each Layer**
   ```
   Layer 0 (Sky):
   - Layer Object: Background_Sky
   - Scroll Speed: 0.5
   
   Layer 1 (Mountains):
   - Layer Object: Background_Mountains
   - Scroll Speed: 1.5
   
   Layer 2 (Ground):
   - Layer Object: Background_Ground
   - Scroll Speed: 3.0
   ```

6. **Adjust Settings**
   - Global Speed Multiplier: `1.0` (increase for faster game)
   - Use Game Speed: Checked

---

## 🎯 Recommended Settings for Your Game

Based on your pipe speed (`5.0f`), I recommend:

### For Simple Parallax:
```
Parallax Speed: 0.15 (subtle background movement)
```

### For Multi-Layer Parallax:
```
Sky Layer: 0.3
Cloud Layer: 0.8
Ground Layer: 2.0
Global Speed Multiplier: 1.0
```

---

## 🖼️ Where to Get Background Assets

### Free Options:
1. **OpenGameArt.org** - Free game assets
2. **Kenney.nl** - Free parallax backgrounds
3. **itch.io** (search "free parallax background")
4. **Unity Asset Store** (filter by Free)

### Quick DIY:
- Create gradient sky in Photoshop/GIMP
- Add simple cloud/mountain silhouettes
- Make sure image tiles seamlessly (repeat pattern)

---

## 🔧 Troubleshooting

### Background doesn't scroll:
- Check that material Wrap Mode is set to `Repeat`
- Verify the script is attached and enabled
- Check that game isn't in Game Over state

### Seams visible (Multi-Layer):
- Ensure duplicate sprites are positioned exactly at sprite width
- Check that both originals and duplicates have same scale
- Layer width is calculated from sprite bounds

### Too fast/slow:
- Adjust `parallaxSpeed` or `scrollSpeed` values
- Try `globalSpeedMultiplier` to affect all layers at once

---

## ✨ Integration with Your Game

Both scripts automatically:
- ✅ Stop scrolling when game is over (via LogicScript)
- ✅ Use Time.deltaTime for smooth, frame-independent movement
- ✅ Include error handling and null checks
- ✅ Match your existing code style and structure

---

## 🎮 Quick Start (Minimal Setup)

If you just want to test quickly:

1. Create a Sprite object called "Background"
2. Assign any texture (even FlappyBird.png for testing)
3. Change texture Import Settings → Wrap Mode → `Repeat`
4. Add `ParallaxBackground` component
5. Set Parallax Speed to `0.2`
6. Press Play!

The background will scroll continuously and stop when you die.

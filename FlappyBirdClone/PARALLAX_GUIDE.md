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
   
   For each layer (Sky, Mountains, Ground, etc.):
   
   a. Create a **Parent Empty GameObject**:
      - Right-click Hierarchy → Create Empty
      - Name it: "Layer_Sky", "Layer_Mountains", "Layer_Ground"
   
   b. Create **Sprite Children** (2 or more for seamless loop):
      - Right-click parent → 2D Object → Sprite
      - Name them: "Sky_1", "Sky_2"
      - Assign your background texture
   
   c. Position sprites side-by-side:
      - Sky_1: X=0, Y=0
      - Sky_2: X=(sprite width), Y=0
      - **Important**: They must touch perfectly with no gaps!

2. **Position Layer Parents**
   - Layer_Sky: X=0, Y=0, Z=10 (furthest back)
   - Layer_Mountains: X=0, Y=-2, Z=9
   - Layer_Ground: X=0, Y=-4, Z=8 (closest)

3. **Add Multi-Layer Script**
   - Create an Empty GameObject: "ParallaxManager"
   - Add Component → `MultiLayerParallax`
   - Set **Layers array size** to 3 (or however many layers you have)

4. **Configure Each Layer**
   
   **Example with 3 layers:**
   
   ```
   Layer 0 (Sky):
   - Layer Parent: Layer_Sky (optional, can leave empty)
   - Layer Objects: Size = 2
     - Element 0: Sky_1
     - Element 1: Sky_2
   - Scroll Speed: 0.5
   
   Layer 1 (Mountains):
   - Layer Parent: Layer_Mountains
   - Layer Objects: Size = 2
     - Element 0: Mountains_1
     - Element 1: Mountains_2
   - Scroll Speed: 1.5
   
   Layer 2 (Ground):
   - Layer Parent: Layer_Ground
   - Layer Objects: Size = 3 (you can have different amounts!)
     - Element 0: Ground_1
     - Element 1: Ground_2
     - Element 2: Ground_3
   - Scroll Speed: 3.0
   ```

5. **Adjust Settings**
   - **Global Speed Multiplier**: `1.0` (affects all layers proportionally)
   - **Despawn Buffer**: `5.0` (how far past the left camera edge before teleporting)
   - **Respawn Buffer**: `0.5` (extra spacing when respawning on the right)
   
   **What these do:**
   - **Despawn Buffer**: Larger = sprites stay visible longer before teleporting (prevents early disappearance)
   - **Respawn Buffer**: Adds gap between respawned sprite and the rightmost sprite (prevents overlap)
   
   **Recommended values:**
   - For smooth seamless scrolling: Despawn Buffer = `5-10`, Respawn Buffer = `0-1`
   - If you see gaps appearing: Reduce Respawn Buffer to `0` or negative values like `-0.5`
   - If sprites disappear too early: Increase Despawn Buffer to `10-15`

### 🎯 Key Points:
- ✅ **Each layer can have DIFFERENT numbers of objects** (2, 3, 4, etc.)
- ✅ **Mix and match**: Sky with 2 sprites, mountains with 3, ground with 4
- ✅ **Automatic looping**: Objects teleport to the right when they move off-screen
- ✅ **Layer Parent is optional**: Only used for organization

---

## 📐 Visual Setup Example

```
Hierarchy Structure:
├── ParallaxManager (MultiLayerParallax script)
├── Layer_Sky (Empty GameObject - optional parent)
│   ├── Sky_1 (Sprite at X=0)
│   └── Sky_2 (Sprite at X=20)  ← duplicate positioned right
├── Layer_Mountains (Empty GameObject)
│   ├── Mountains_1 (Sprite at X=0)
│   └── Mountains_2 (Sprite at X=20)
└── Layer_Ground (Empty GameObject)
    ├── Ground_1 (Sprite at X=0)
    ├── Ground_2 (Sprite at X=20)
    └── Ground_3 (Sprite at X=40)  ← you can have MORE!
```

**Inspector Setup for ParallaxManager:**
```
MultiLayerParallax Component:
├── Layers [Array Size: 3]
│   ├── Element 0 (Sky Layer)
│   │   ├── Layer Parent: Layer_Sky (optional)
│   │   ├── Layer Objects [Array Size: 2]
│   │   │   ├── Element 0: Sky_1
│   │   │   └── Element 1: Sky_2
│   │   └── Scroll Speed: 0.5
│   │
│   ├── Element 1 (Mountains Layer)
│   │   ├── Layer Objects [Array Size: 2]
│   │   │   ├── Element 0: Mountains_1
│   │   │   └── Element 1: Mountains_2
│   │   └── Scroll Speed: 1.5
│   │
│   └── Element 2 (Ground Layer)
│       ├── Layer Objects [Array Size: 3]
│       │   ├── Element 0: Ground_1
│       │   ├── Element 1: Ground_2
│       │   └── Element 2: Ground_3
│       └── Scroll Speed: 3.0
│
├── Global Speed Multiplier: 1.0
├── Despawn Buffer: 5.0
└── Respawn Buffer: 0.5
```

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

### Seams/Gaps visible (Multi-Layer):
- **Most common issue**: Sprites aren't positioned correctly
- Measure your sprite width (select sprite → see Bounds in Inspector)
- Position second sprite EXACTLY at first sprite's width
- Example: If bounds.size.x = 19.2, place second sprite at X=19.2
- **NEW**: Try adjusting `Respawn Buffer` - set to `0` or even negative (`-0.5`) for tighter gaps

### Objects disappear too early:
- **Solution**: Increase `Despawn Buffer` from `5` to `10` or `15`
- This gives sprites more buffer before they teleport
- The script now calculates based on camera view automatically!

### Objects teleport/respawn in view:
- **Solution**: Increase `Despawn Buffer` (sprites despawn too close to camera)
- **Solution**: Reduce `Respawn Buffer` if gaps appear between tiles
- The thresholds are now dynamic based on camera position

### Gaps appear between respawned sprites:
- **Solution**: Reduce `Respawn Buffer` to `0`, `-0.5`, or even `-1.0`
- Negative values make sprites overlap slightly for seamless appearance

### Different layers have different numbers of sprites:
- ✅ **This is totally fine!** Each layer is independent
- Sky can have 2 sprites, ground can have 4 sprites
- Just fill in the correct array size for each layer

### How many sprites do I need per layer?
- **Minimum**: 2 sprites (for basic looping)
- **Recommended**: 3 sprites (smoother, less noticeable reset)
- **Formula**: `Number of sprites = (Camera width / Sprite width) + 1`

### Too fast/slow:
- Adjust `scrollSpeed` values per layer
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

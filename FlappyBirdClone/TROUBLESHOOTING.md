# Parallax Troubleshooting - Not Seeing Sprites

## 🔍 Quick Diagnostic Steps

### Step 1: Add Debug Script
1. Select your **ParallaxManager** GameObject
2. Add Component → **ParallaxDebug**
3. Press Play
4. You should see debug info in the top-left corner

This will show you:
- How many layers you have
- How many objects in each layer
- Position of each sprite
- Whether sprites have SpriteRenderer components

---

## 🎯 Common Issues & Solutions

### Issue 1: Sprites are positioned off-screen

**Symptoms:** Debug shows sprites exist but you don't see them

**Solutions:**
- Check sprite Z position - should be POSITIVE (e.g., Z=5, Z=8, Z=10)
- If Z is negative or 0, sprites might be behind the camera or other objects
- Camera is usually at Z=-10, so sprites need Z > -10

**Fix:**
```
Select each sprite → Set Z position to:
- Sky layer: Z=10 (furthest)
- Middle layer: Z=8
- Ground layer: Z=5 (closest)
```

---

### Issue 2: No sprites assigned to ParallaxManager

**Symptoms:** Debug shows "No layers configured" or "No objects in layer"

**Fix:**
1. Select **ParallaxManager**
2. Find **MultiLayerParallax** component
3. Set **Layers** array size (e.g., 4 for 4 layers)
4. For each layer:
   - Set **Layer Objects** array size (usually 2-3)
   - Drag your sprite GameObjects into the Element slots
   - Set **Scroll Speed** (e.g., 0.5, 1.0, 2.0, 3.0)

---

### Issue 3: Sprites don't have SpriteRenderer

**Symptoms:** Debug shows "No SpriteRenderer"

**Fix:**
1. Select each sprite GameObject
2. If no SpriteRenderer exists:
   - Add Component → Rendering → Sprite Renderer
3. Assign your image:
   - Sprite: Drag your .png file here (1.png, 2.png, etc.)

---

### Issue 4: Sprites are too small or too large

**Fix:**
- Select sprite → Adjust **Transform Scale** (try 5, 10, or 20)
- Or select the .png in Assets → Set **Pixels Per Unit** (try 100, 50, or 32)

---

### Issue 5: Sprites are the same color as background

**Fix:**
- Camera background might match your sprites
- Change **Camera → Background Color** to something different
- Or add a contrasting temporary sprite to test

---

## ✅ Correct Setup Checklist

```
□ ParallaxManager GameObject exists
□ MultiLayerParallax script attached to ParallaxManager
□ Layers array size is set (not 0)
□ Each layer has layerObjects array size set (not 0)
□ Sprite GameObjects are dragged into layer object slots
□ Each sprite has SpriteRenderer component
□ Each sprite has an image assigned in SpriteRenderer
□ Sprite Z positions are POSITIVE (5, 8, 10, etc.)
□ Sprites are positioned where camera can see them
□ Camera exists and is tagged "MainCamera"
```

---

## 🎮 Quick Test Setup

If nothing works, try this minimal setup:

1. **Create sprite manually:**
   ```
   Hierarchy → Right-click → 2D Object → Sprite → Sprite Renderer
   Name it: TestSprite
   Position: X=0, Y=0, Z=5
   Scale: 10, 10, 1
   Assign Sprite: 1.png
   ```

2. **Duplicate it:**
   ```
   Duplicate TestSprite (Ctrl+D)
   Position: X=20, Y=0, Z=5
   ```

3. **Setup ParallaxManager:**
   ```
   Create Empty GameObject: ParallaxManager
   Add Component: MultiLayerParallax
   
   Layers size: 1
   Layer 0:
     - Layer Objects size: 2
     - Element 0: TestSprite
     - Element 1: TestSprite (1)  [the duplicate]
     - Scroll Speed: 1.0
   
   Global Speed Multiplier: 1.0
   ```

4. **Press Play** - you should see the sprites scrolling!

---

## 🐛 Still Not Working?

Check these Unity Editor settings:

1. **Game View Camera:**
   - Make sure Game view is showing your scene
   - Click on Game tab (not Scene tab)

2. **Camera Position:**
   - Select Main Camera
   - Position should be around X=0, Y=0, Z=-10

3. **Sorting Layers:**
   - Select sprite → Sprite Renderer → Order in Layer: 0
   - If you have other UI blocking it, increase this number

4. **Camera Culling Mask:**
   - Select Main Camera
   - Culling Mask: Everything (default)

---

## 📹 Video Tutorial Alternative

If you're visual learner, search YouTube for:
"Unity 2D Parallax Background Tutorial"

The basic concept is always:
1. Create sprite GameObjects
2. Position them side-by-side
3. Move them left every frame
4. When off-screen, teleport to the right

# Hướng Dẫn Hoàn Chỉnh - Flappy Bird Clone
## Complete Setup Guide with Visual Instructions

---

## 📋 Mục Lục / Table of Contents

1. [Cài Đặt Ban Đầu / Initial Setup](#1-cài-đặt-ban-đầu)
2. [Tạo Bird (Con Chim)](#2-tạo-bird-con-chim)
3. [Tạo Pipes (Ống Nước)](#3-tạo-pipes-ống-nước)
4. [Thêm Parallax Background](#4-thêm-parallax-background)
5. [Thêm Âm Thanh / Audio](#5-thêm-âm-thanh)
6. [Tạo Start Screen](#6-tạo-start-screen)
7. [Tạo Settings Panel](#7-tạo-settings-panel)
8. [Game Over Screen](#8-game-over-screen)

---

## 1. Cài Đặt Ban Đầu / Initial Setup

### Bước 1.1: Tạo Project Unity
```
1. Mở Unity Hub
2. Click "New Project"
3. Chọn template: "2D Core"
4. Đặt tên: "FlappyBirdClone"
5. Click "Create"
```

**Hình ảnh mô tả:**
```
┌─────────────────────────────────┐
│    Unity Hub - New Project      │
├─────────────────────────────────┤
│ Template: [2D Core] ✓           │
│ Project Name: FlappyBirdClone   │
│ Location: D:\Projects\          │
│                                 │
│         [Create Project]        │
└─────────────────────────────────┘
```

### Bước 1.2: Cài Đặt Input System
```
1. Window → Package Manager
2. Tìm "Input System"
3. Click "Install"
4. Khi có popup "Enable Backend" → Click "Yes"
5. Unity sẽ restart
```

**Vị trí trong menu:**
```
Unity Editor
├── Window
│   └── Package Manager
│       └── Unity Registry
│           └── Input System [Install]
```

---

## 2. Tạo Bird (Con Chim)

### Bước 2.1: Tạo Bird GameObject
```
1. Hierarchy → Right Click → Create Empty
2. Đặt tên: "Bird"
3. Add Component → Sprite Renderer
4. Kéo sprite chim vào "Sprite" field
5. Position: X=0, Y=0, Z=0
```

**Inspector Settings:**
```
┌─────────────────────────────┐
│ Bird                        │
├─────────────────────────────┤
│ Transform                   │
│  Position: (0, 0, 0)        │
│  Rotation: (0, 0, 0)        │
│  Scale: (1, 1, 1)           │
├─────────────────────────────┤
│ Sprite Renderer             │
│  Sprite: [FlappyBird.png]   │
│  Color: White               │
│  Order in Layer: 1          │
└─────────────────────────────┘
```

### Bước 2.2: Thêm Physics
```
1. Select Bird
2. Add Component → Rigidbody 2D
3. Settings:
   - Gravity Scale: 5
   - Linear Drag: 0
   - Angular Drag: 0
   - Collision Detection: Continuous
```

### Bước 2.3: Thêm Collider
```
1. Add Component → Circle Collider 2D
2. Adjust radius to fit bird sprite
3. Đừng quên TAG!
```

**QUAN TRỌNG - Tag Bird:**
```
1. Select Bird
2. Top of Inspector → Tag dropdown
3. Chọn "Player"
   (Nếu không có "Player", tạo mới: Add Tag...)
```

### Bước 2.4: Thêm Bird Script
```
1. Assets → Right Click → Create → C# Script
2. Tên: "Bird"
3. Copy code từ Assets\Bird.cs
4. Kéo script vào Bird GameObject
5. Assign references:
   - Rb2d: Kéo Rigidbody2D vào đây
```

**Visual:**
```
Bird GameObject
├── Transform
├── Sprite Renderer ✓
├── Rigidbody 2D ✓
├── Circle Collider 2D ✓
└── Bird (Script) ✓
    ├── Rb2d: [Rigidbody2D]
    ├── Flap Strength: 14
    ├── Max Fall Speed: 8
    └── Tilt Speed: 2
```

---

## 3. Tạo Pipes (Ống Nước)

### Bước 3.1: Tạo Pipe Prefab
```
1. Hierarchy → Right Click → 2D Object → Sprite
2. Tên: "Pipe"
3. Sprite Renderer → Sprite: Pipe.png
4. Scale: Adjust cho phù hợp (thường 2-3 units cao)
```

### Bước 3.2: Tạo Pipe Set (Cặp Ống)
```
1. Create Empty → Tên: "PipeSet"
2. Tạo 2 Pipes làm con:
   - PipeTop (Position: Y=3)
   - PipeBottom (Position: Y=-3)
3. Rotate PipeTop 180 độ (Z rotation)
```

**Cấu trúc:**
```
PipeSet (Empty GameObject)
├── PipeTop (Sprite)
│   └── Rotation: (0, 0, 180)
└── PipeBottom (Sprite)
    └── Rotation: (0, 0, 0)
```

### Bước 3.3: Thêm Colliders
```
Cho TỪNG pipe (Top và Bottom):
1. Add Component → Box Collider 2D
2. Adjust size to fit sprite
```

### Bước 3.4: Tạo Trigger Zone (Vùng Ghi Điểm)
```
1. PipeSet → Right Click → Create Empty
2. Tên: "ScoreZone"
3. Add Component → Box Collider 2D
4. Settings:
   - Is Trigger: ✓ (CHECKED!)
   - Size: Rộng đủ, cao từ top đến bottom pipe
5. Add Component → TriggerScript
```

**Visual ScoreZone:**
```
┌─────────────────┐
│    PipeTop      │ ← Collider (solid)
│─────────────────│
│                 │
│   ScoreZone     │ ← Trigger (ghi điểm)
│   (invisible)   │
│                 │
│─────────────────│
│   PipeBottom    │ ← Collider (solid)
└─────────────────┘
```

### Bước 3.5: Thêm Scripts
```
1. Select PipeSet
2. Add Component → PipeMove
   - Move Speed: 5
3. Kéo PipeSet vào Assets để tạo Prefab
```

### Bước 3.6: Tạo Pipe Spawner
```
1. Hierarchy → Create Empty → "PipeSpawner"
2. Position: X=15 (bên phải màn hình)
3. Add Component → PipeSpawner
4. Settings:
   - Pipe Prefab: Kéo PipeSet prefab vào đây
   - Spawn Interval: 2
   - Initial Delay: 0
   - Spawn Offset X: 5
   - Min Height: -1
   - Max Height: 3
```

---

## 4. Thêm Parallax Background

### Bước 4.1: Import Background Sprites
```
1. Kéo 4 ảnh background vào Assets (1.png, 2.png, 3.png, 4.png)
2. Select tất cả → Inspector
3. Texture Type: Sprite (2D and UI)
4. Pixels Per Unit: 100
5. Filter Mode: Point (no filter) - cho pixel art
6. Apply
```

### Bước 4.2: Tạo Layer Structure
```
Hierarchy → Create Empty → "ParallaxManager"

Tạo 4 layers:
1. Layer_Sky (Z=10)
2. Layer_Mountains (Z=9)
3. Layer_Ground (Z=8)
4. Layer_Foreground (Z=7)
```

### Bước 4.3: Tạo Sprites Cho Mỗi Layer
```
VÍ DỤ: Layer_Sky
1. Right Click Layer_Sky → 2D Object → Sprite
2. Tên: "Sky_1"
3. Sprite Renderer → Sprite: 1.png
4. Scale cho phù hợp với màn hình
5. Position: X=0

6. Duplicate (Ctrl+D)
7. Tên: "Sky_2"
8. Position: X = (chiều rộng của Sky_1)
   - Ví dụ: Nếu Sky_1 rộng 25 units → X=25
```

**Lặp lại cho 3 layers còn lại!**

### Bước 4.4: Setup Parallax Script
```
1. Select ParallaxManager
2. Add Component → MultiLayerParallax
3. Layers: Size = 4
4. Setup từng layer:

Layer 0 (Sky):
├── Layer Objects: Size = 2
│   ├── Element 0: Sky_1
│   └── Element 1: Sky_2
└── Scroll Speed: 0.3

Layer 1 (Mountains):
├── Layer Objects: Size = 2
│   ├── Element 0: Mountains_1
│   └── Element 1: Mountains_2
└── Scroll Speed: 0.8

Layer 2 (Ground):
├── Layer Objects: Size = 2
│   ├── Element 0: Ground_1
│   └── Element 1: Ground_2
└── Scroll Speed: 2.0

Layer 3 (Foreground):
├── Layer Objects: Size = 2
└── Scroll Speed: 3.0

Settings:
├── Global Speed Multiplier: 1
├── Despawn X: -26
└── Respawn Gap: 0
```

---

## 5. Thêm Âm Thanh / Audio

### Bước 5.1: Import Sound Files
```
1. Tạo folder: Assets → Audio
2. Kéo các file âm thanh vào:
   - flap.wav
   - hit.wav
   - score.wav
   - gameover.wav
   - music.mp3
```

### Bước 5.2: Tạo AudioManager
```
1. Hierarchy → Create Empty → "AudioManager"
2. Add Component → AudioManager
3. Script sẽ tự động tạo 2 AudioSource components
```

### Bước 5.3: Assign Sounds
```
AudioManager Inspector:
├── SFX Source: [Auto-created]
├── Music Source: [Auto-created]
├── Sound Effects:
│   ├── Flap Sound: [flap.wav]
│   ├── Hit Sound: [hit.wav]
│   ├── Score Sound: [score.wav]
│   └── Game Over Sound: [gameover.wav]
├── Music:
│   └── Background Music: [music.mp3]
├── Volume Settings:
│   ├── SFX Volume: 0.7
│   └── Music Volume: 0.5
└── Settings:
    ├── Play Music On Start: ✓
    └── Music Fade Duration: 1.5
```

---

## 6. Tạo Start Screen

### Bước 6.1: Tạo Canvas
```
1. Hierarchy → Right Click → UI → Canvas
2. Canvas Scaler Settings:
   - UI Scale Mode: Scale With Screen Size
   - Reference Resolution: 1920 x 1080
```

### Bước 6.2: Tạo Start Screen Panel
```
1. Right Click Canvas → UI → Panel
2. Tên: "StartScreenUI"
3. Màu nền: Tùy chọn (hoặc trong suốt)
```

### Bước 6.3: Thêm Title Text
```
1. Right Click StartScreenUI → UI → Text
2. Tên: "TitleText"
3. Settings:
   - Text: "FLAPPY BIRD"
   - Font Size: 100
   - Alignment: Center (cả horizontal và vertical)
   - Color: Yellow hoặc màu bạn thích
   - Position: Top-center của panel
```

### Bước 6.4: Thêm Instructions Text
```
1. Right Click StartScreenUI → UI → Text
2. Tên: "InstructionsText"
3. Settings:
   - Text: "Nhấn Phím Bất Kỳ Để Chơi"
   - Font Size: 50
   - Alignment: Center
   - Position: Bottom-center của panel
   - OPTIONAL: Add Component → PulseText (để text nhấp nháy)
```

**Layout Visual:**
```
┌──────────────────────────┐
│                          │
│    FLAPPY BIRD           │ ← TitleText (lớn)
│                          │
│         🐦               │ ← Bird (bobbing)
│                          │
│  Nhấn Phím Để Chơi       │ ← Instructions (nhấp nháy)
│                          │
└──────────────────────────┘
```

### Bước 6.5: Setup StartScreen Script
```
1. Create Empty → "GameManager"
2. Add Component → SimpleStartScreen
3. Assign:
   - Score Text: Kéo ScoreText vào đây
   - Start Screen UI: Kéo StartScreenUI vào đây
```

---

## 7. Tạo Settings Panel

### Bước 7.1: Tạo Settings Panel
```
1. Right Click Canvas → UI → Panel
2. Tên: "SettingsPanel"
3. Size: Khoảng 400x300
4. Màu nền: Semi-transparent dark
```

### Bước 7.2: Thêm Title
```
1. Right Click SettingsPanel → UI → Text
2. Tên: "SettingsTitle"
3. Text: "CÀI ĐẶT / SETTINGS"
4. Font Size: 40
5. Position: Top của panel
```

### Bước 7.3: Tạo SFX Slider
```
1. Right Click SettingsPanel → UI → Slider
2. Tên: "SFXSlider"

Thêm label:
3. Right Click SettingsPanel → UI → Text
4. Tên: "SFXLabel"
5. Text: "Âm Thanh / Sound Effects"
6. Position: Phía trên slider

Thêm volume display:
7. Right Click SettingsPanel → UI → Text
8. Tên: "SFXVolumeText"
9. Text: "70%"
10. Position: Bên phải slider
```

### Bước 7.4: Tạo Music Slider
```
Lặp lại như SFX Slider:
1. MusicSlider
2. MusicLabel: "Nhạc Nền / Music"
3. MusicVolumeText: "50%"
```

### Bước 7.5: Thêm Close Button
```
1. Right Click SettingsPanel → UI → Button
2. Tên: "CloseButton"
3. Text: "ĐÓNG / CLOSE"
4. Position: Bottom của panel
```

### Bước 7.6: Thêm Settings Button trên Start Screen
```
1. Right Click StartScreenUI → UI → Button
2. Tên: "SettingsButton"
3. Text: "⚙️" hoặc "CÀI ĐẶT"
4. Position: Top-right corner
```

### Bước 7.7: Setup Script
```
1. Create Empty → "SettingsManager"
2. Add Component → SettingsPanel
3. Assign tất cả references:
   - Settings Panel: SettingsPanel
   - SFX Slider: SFXSlider
   - Music Slider: MusicSlider
   - Close Button: CloseButton
   - SFX Volume Text: SFXVolumeText
   - Music Volume Text: MusicVolumeText

4. Settings Button → OnClick():
   - Kéo SettingsManager vào
   - Function: SettingsPanel → OpenSettings()

5. Close Button đã tự động setup trong script
```

**Layout Visual:**
```
┌────────────────────────────┐
│     CÀI ĐẶT / SETTINGS     │
├────────────────────────────┤
│ Âm Thanh / Sound Effects   │
│ ━━━━━━●━━━━━━  70%         │
│                            │
│ Nhạc Nền / Music           │
│ ━━━━●━━━━━━━━  50%         │
│                            │
│      [ĐÓNG / CLOSE]        │
└────────────────────────────┘
```

---

## 8. Game Over Screen

### Bước 8.1: Tạo Game Over Panel
```
1. Right Click Canvas → UI → Panel
2. Tên: "GameOverScreen"
3. Initially: Inactive (uncheck ở Inspector)
```

### Bước 8.2: Thêm Text
```
1. Right Click GameOverScreen → UI → Text
2. Tên: "GameOverText"
3. Settings:
   - Text: "GAME OVER"
   - Font Size: 80
   - Color: Red
   - Alignment: Center
```

### Bước 8.3: Thêm Restart Button
```
1. Right Click GameOverScreen → UI → Button
2. Tên: "RestartButton"
3. Text: "CHƠI LẠI / RESTART"
```

### Bước 8.4: Tạo Logic Manager
```
1. Create Empty → "LogicManager"
2. Tag: "Logic" (QUAN TRỌNG!)
3. Add Component → LogicScript
4. Assign:
   - Score Text: Kéo score text vào
   - Game Over Screen: Kéo GameOverScreen vào

5. RestartButton → OnClick():
   - Kéo LogicManager vào
   - Function: LogicScript → restartGame()
```

---

## 9. Kiểm Tra Hoàn Tất / Final Checklist

### 9.1: Hierarchy Structure Hoàn Chỉnh
```
Scene Hierarchy
├── Main Camera
├── Canvas
│   ├── StartScreenUI (Panel)
│   │   ├── TitleText
│   │   ├── InstructionsText
│   │   └── SettingsButton
│   ├── GameplayUI (Panel)
│   │   └── ScoreText
│   ├── SettingsPanel (Panel)
│   │   ├── SettingsTitle
│   │   ├── SFXLabel
│   │   ├── SFXSlider
│   │   ├── SFXVolumeText
│   │   ├── MusicLabel
│   │   ├── MusicSlider
│   │   ├── MusicVolumeText
│   │   └── CloseButton
│   └── GameOverScreen (Panel)
│       ├── GameOverText
│       └── RestartButton
├── EventSystem
├── Bird (Tagged: Player)
│   └── Bird (Script)
├── ParallaxManager
│   ├── Layer_Sky
│   ├── Layer_Mountains
│   ├── Layer_Ground
│   └── Layer_Foreground
├── PipeSpawner
├── AudioManager
├── GameManager (SimpleStartScreen)
├── SettingsManager (SettingsPanel)
└── LogicManager (Tagged: Logic)
```

### 9.2: Checklist Scripts
```
✓ Bird.cs - Gắn vào Bird
✓ PipeMove.cs - Gắn vào PipeSet prefab
✓ PipeSpawner.cs - Gắn vào PipeSpawner
✓ TriggerScript.cs - Gắn vào ScoreZone
✓ LogicScript.cs - Gắn vào LogicManager
✓ MultiLayerParallax.cs - Gắn vào ParallaxManager
✓ AudioManager.cs - Gắn vào AudioManager
✓ SimpleStartScreen.cs - Gắn vào GameManager
✓ SettingsPanel.cs - Gắn vào SettingsManager
✓ PulseText.cs - (Optional) Gắn vào InstructionsText
```

### 9.3: Checklist Tags
```
✓ Bird → Tag: "Player"
✓ LogicManager → Tag: "Logic"
✓ Main Camera → Tag: "MainCamera"
```

### 9.4: Test Game
```
1. Press Play
2. Kiểm tra:
   ✓ Start screen hiện ra
   ✓ Bird không rơi
   ✓ Nhấn phím → game bắt đầu
   ✓ Bird bay được (Space)
   ✓ Pipes di chuyển
   ✓ Ghi điểm khi qua pipe
   ✓ Va chạm → game over
   ✓ Nhạc nền chơi
   ✓ Sound effects hoạt động
   ✓ Settings mở được
   ✓ Volume điều chỉnh được
   ✓ Restart hoạt động
```

---

## 10. Troubleshooting / Xử Lý Lỗi Thường Gặp

### Lỗi 1: Bird không bay
```
Kiểm tra:
□ Bird có tag "Player"?
□ Rigidbody2D Gravity Scale > 0?
□ Bird script assigned đúng?
□ Input System đã cài?
```

### Lỗi 2: Không ghi điểm
```
Kiểm tra:
□ ScoreZone có Box Collider 2D?
□ Is Trigger = CHECKED?
□ TriggerScript gắn vào ScoreZone?
□ Bird có tag "Player"?
□ LogicManager có tag "Logic"?
```

### Lỗi 3: Parallax không chạy
```
Kiểm tra:
□ Sprites positioned đúng (X=0, X=width)?
□ Layer Objects assigned trong script?
□ Scroll Speed > 0?
□ Sprites ở đúng Z depth?
```

### Lỗi 4: Không có âm thanh
```
Kiểm tra:
□ AudioManager tồn tại trong scene?
□ Sound clips assigned?
□ Volume > 0?
□ Unity Editor không mute?
```

### Lỗi 5: Input System error
```
Lỗi: "You are trying to read Input using the UnityEngine.Input class..."

Fix:
1. Edit → Project Settings → Player
2. Active Input Handling: "Input System Package (New)"
3. Hoặc: "Both" nếu cần cả 2
4. Restart Unity
```

---

## 11. Tips & Tricks

### Tip 1: Chỉnh Game Feel
```
Bird.cs:
- Tăng Flap Strength = nhảy cao hơn
- Tăng Gravity Scale = rơi nhanh hơn
- Giảm Max Fall Speed = không rơi quá nhanh

PipeMove.cs:
- Tăng Move Speed = khó hơn
- Giảm = dễ hơn

PipeSpawner.cs:
- Giảm Spawn Interval = nhiều pipes hơn
- Tăng Min/Max Height = biến đổi nhiều hơn
```

### Tip 2: Tùy Chỉnh Màu Sắc
```
1. Chọn sprite
2. Sprite Renderer → Color
3. Chọn màu mới
4. Hoặc dùng Color Overlay shader
```

### Tip 3: Thêm Particles
```
1. Right Click Bird → Effects → Particle System
2. Preset: "Fire" hoặc "Sparkles"
3. Play on Awake: No
4. Trigger trong Bird.Flap()
```

---

## 12. Xuất Game / Build Game

### Build cho Windows
```
1. File → Build Settings
2. Platform: PC, Mac & Linux Standalone
3. Target Platform: Windows
4. Architecture: x86_64
5. Add Open Scenes
6. Player Settings:
   - Company Name: Your Name
   - Product Name: Flappy Bird Clone
   - Icon: Assign your icon
7. Build
```

### Build cho Android
```
1. File → Build Settings
2. Platform: Android
3. Switch Platform (đợi)
4. Player Settings:
   - Package Name: com.yourname.flappybird
   - Minimum API Level: 21
   - Target API Level: 33
5. Build
```

### Build cho WebGL
```
1. File → Build Settings
2. Platform: WebGL
3. Switch Platform
4. Build
5. Upload to itch.io hoặc host riêng
```

---

## 🎉 HOÀN TẤT!

Chúc mừng! Bạn đã hoàn thành Flappy Bird Clone!

### Next Steps:
- Thêm high score system
- Thêm nhiều obstacles
- Tạo power-ups
- Thêm skin cho bird
- Tạo leaderboard
- Thêm achievements

### Resources:
- Unity Documentation: docs.unity3d.com
- Free Assets: opengameart.org, kenney.nl
- Free Sounds: freesound.org

**Good luck with your game! 🚀🎮**

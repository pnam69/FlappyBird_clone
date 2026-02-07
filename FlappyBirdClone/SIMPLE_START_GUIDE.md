# Simple Start Screen - 1 Minute Setup

## 🚀 Super Quick Setup (3 Steps!)

Forget all that complex UI stuff. Here's the **easiest** way:

### Step 1: Add Script (30 seconds)
1. Create Empty GameObject → Name it "GameStart"
2. Add Component → **SimpleStartScreen**
3. Drag your **Score Text** into the script slot

### Step 2: Tag Your Bird (10 seconds)
1. Select Bird GameObject
2. Tag dropdown → Select "Player"
3. Done!

### Step 3: Adjust Gravity (20 seconds)
1. Play the game once
2. If bird falls too fast/slow after starting:
   - Select GameStart
   - Find "Start Game()" section in script
   - Change `gravityScale = 3` to match your bird's gravity

**That's it!** 🎉

---

## 🎯 What It Does

1. **Game starts frozen** - Bird doesn't fall
2. **Shows "Press Any Key to Start"** on your score text
3. **Press any key** - Game begins, bird falls normally
4. **Score appears** - Back to normal

---

## ⚙️ How It Works

- Uses your **existing Score Text** (no new UI needed!)
- Finds bird automatically by "Player" tag
- Temporarily changes score text to show instructions
- When game starts, everything goes back to normal

---

## 🔧 Troubleshooting

### Bird still falls at start:
- Make sure bird is tagged "Player"
- Check that SimpleStartScreen script is active

### Score text doesn't show:
- Assign your Score Text in SimpleStartScreen component
- Make sure Score Text exists in scene

### Bird falls too fast/slow:
- Adjust `gravityScale = 3` in the script
- Match it to your bird's Rigidbody2D → Gravity Scale

---

## 💡 Want More Features?

If you want a proper start screen later:
- Use the full **StartScreen.cs** system
- Follow the detailed guide

But this works perfectly for a quick prototype! 🚀

---

## ✅ Complete Checklist

```
□ Create "GameStart" GameObject
□ Add SimpleStartScreen script
□ Assign Score Text
□ Tag bird as "Player"
□ Test - bird should freeze
□ Press key - bird should start falling
```

**Total time: 1 minute!** ⚡

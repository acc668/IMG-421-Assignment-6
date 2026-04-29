# IMG-421-Assignment-6: Space Shooter
 
A browser-based space shooter game built in Unity, featuring three difficulty levels, enemy formations, power-up weapons, and a fully animated starfield menu screen.
 
---
 
## How to Play
 
- **Move** — Arrow Keys or WASD
- **Shoot** — Space Bar
- **Goal** — Destroy as many enemy ships as possible before losing all your lives
### Shield & Lives
Your shield absorbs the first hits before you lose a life. Once your shield is gone, each hit costs a life. Lose all your lives and it's game over.
 
| Difficulty | Lives | Shield | Enemies |
|---|---|---|---|
| Easy | 3 | ★★★ | Slow, single spawns |
| Medium | 3 | ★★ | Faster, spawn in formations |
| Hard | 2 | ★ | Fast, formations, shoot back |
 
---
 
## Features
 
- Main menu with animated starfield background
- Three difficulty levels with meaningfully different gameplay
- Enemy formation spawning on Medium and Hard
- Enemies shoot back on Hard difficulty
- Score tracking with persistent high score
- Game over screen with Play Again, Main Menu, and Quit options
- Shield visual that changes color (green → yellow → red)
- Spaceship visuals built from primitives for Hero and Enemy
- Glowing laser bolt projectiles
---
 
## Scripts Overview
 
| Script | Purpose |
|---|---|
| `Hero.cs` | Player movement, shooting, shield/lives logic |
| `Enemy.cs` | Base enemy — moves down, takes damage, can shoot |
| `Main.cs` | Spawns enemies, reads difficulty settings |
| `GameManager.cs` | Score, lives, HUD updates, game over screen |
| `MainMenu.cs` | Main menu panel switching and scene loading |
| `DifficultyManager.cs` | Stores selected difficulty, provides settings struct |
| `BoundsCheck.cs` | Detects when objects go off screen |
| `Projectile.cs` | Destroys bullets when off screen |
| `Shield.cs` | Visual shield ring that rotates and changes color |
| `StarfieldBackground.cs` | Spawns animated drifting stars in background |
| `HeroShipBuilder.cs` | Builds hero spaceship from Unity primitives |
| `EnemyShipBuilder.cs` | Builds enemy spaceship from Unity primitives |
| `ProjectileBuilder.cs` | Builds laser bolt visuals for projectiles |
| `Utils.cs` | Shared helper — GetAllMaterials |
 
---
 
## Project Structure
 
```
Assets/
├── Scenes/
│   ├── MainMenu.unity
│   └── _Scene_0.unity
├── Scripts/
│   ├── Hero.cs
│   ├── Enemy.cs
│   ├── Main.cs
│   ├── GameManager.cs
│   ├── MainMenu.cs
│   ├── DifficultyManager.cs
│   ├── BoundsCheck.cs
│   ├── Projectile.cs
│   ├── Shield.cs
│   ├── StarfieldBackground.cs
│   ├── HeroShipBuilder.cs
│   ├── EnemyShipBuilder.cs
│   ├── ProjectileBuilder.cs
│   └── Utils.cs
├── Prefabs/
│   ├── Enemy.prefab
│   └── Projectile.prefab
└── Materials/
```

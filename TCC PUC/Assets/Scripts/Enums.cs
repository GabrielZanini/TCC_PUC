﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeBodyType
{
    System,
    Background,
    Bullet,
    Ship,
    Asteroid,
    PickUp,
    Missile,
    Particle
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public enum Platform
{
    Android,
    Iphone,
    Windows,
    Mac,
    Lunix,
    Playstation,
    Xbox,
    Switch,
}

public enum EffectType
{
    BigExplosion,
    DustExplosion,
    SmallExplosion,
    TinyExplosion,
}

public enum AudioType
{
    Music,
    SFX,
    Voice,
}

public enum GunType
{
    Catling,
    Laser,
    Wave,
}

public enum BulletType
{
    Egg,
    Sphere,
    Drop,
}


public enum ShipType
{
    Player,
    Enemy,
}


public enum AiType
{
    EnemyShip,
    Boss,
    Rotate,
    Snake,
}

public enum LevelState
{
    BeforeStart,
    Playing,
    Paused,
    Stoped,
    Finished,
}
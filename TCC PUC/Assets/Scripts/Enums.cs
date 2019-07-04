using System.Collections;
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
    GigaExplosion,
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
    Asteroid,
    DeathCross,
    SpaceCanon,
    Boss,
}

public enum LevelState
{
    Menu,
    Playing,
    Paused,
    Stoped,
    Finished,
}


public enum PowerUpType
{
    MoreBullets,
    MoreDamage,
    MoreShootingRate,
    Shield,
    Heal,
    Rockets,
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enums
{

    public enum TimeBodyType
    {
        System,
        Background,
        Bullet,
        Ship,
        Asteroid,
        Collectable,
        Missile
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
}

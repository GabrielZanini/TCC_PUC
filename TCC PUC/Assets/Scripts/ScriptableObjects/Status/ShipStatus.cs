using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ShipStatus : ScriptableObject
{
    [Header("Playing")]
    public bool playAfterStop = true;

    [Header("Difficulty")]
    public bool scaleWithDificulty = true;

    [Header("Health")]
    public int hp = 10;

    [Header("Speed")]
    public float speed = 1f;
    public float angularSpeed = 180f;
    public float smoothness = 0.5f;

    [Header("FX")]
    public EffectType deathEffect = EffectType.SmallExplosion;

    [Header("Bullets")]
    public int bullets = 1;
    public int damage = 1;
    [Range(0.01f, 3f)] public float shootingRate = 0.2f;



    public virtual void Clear()
    {
        bullets = 0;
        damage = 0;
        shootingRate = 0;
    }
}

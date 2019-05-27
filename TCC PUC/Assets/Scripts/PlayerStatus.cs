using System.Collections;
using System;
using UnityEngine;

[Serializable]
public class PlayerStatus
{
    [Header("Bullets")]
    public int bullets = 1;
    public int damage = 1;
    public float shootingRate = 0.2f;

    [Header("Shiels")]
    public bool useShield = false;
    public float shildTime = 5f;

    [Header("Rockets")]
    public int  rockets = 1;
    public bool useRockets = false;
    public float rocketsRate = 5f;




    public void Clear()
    {
        bullets = 0;
        damage = 0;
        shootingRate = 0;

        useShield = false;
        shildTime = 0f;

        rockets = 0;
        useRockets = false;
        rocketsRate = 0f;
    }

}


﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsManager : MonoBehaviour
{
    [Header("Game Manager")]
    [SerializeField] GameManager gameManager;
    public GameManager GameManager {
        get { return gameManager; }
        private set { gameManager = value; }
    }

    [Header("Objects")]
    [SerializeField] ObjectPool asteroids;
    public ObjectPool Asteroids {
        get { return asteroids; }
        private set { asteroids = value; }
    }

    [Header("Bullets")]
    [SerializeField] ObjectPool sphereBullets;
    public ObjectPool SphereBullets {
        get { return sphereBullets; }
        private set { sphereBullets = value; }
    }
    [SerializeField] ObjectPool eggBullets;
    public ObjectPool EggBullets {
        get { return eggBullets; }
        private set { eggBullets = value; }
    }
    [SerializeField] ObjectPool dropBullets;
    public ObjectPool DropBullets {
        get { return dropBullets; }
        private set { dropBullets = value; }
    }

    [Header("Particles")]
    [SerializeField] ObjectPool bigExplosion;
    public ObjectPool BigExplosion {
        get { return bigExplosion; }
        private set { bigExplosion = value; }
    }

    [SerializeField] ObjectPool dustExplosion;
    public ObjectPool DustExplosion {
        get { return dustExplosion; }
        private set { dustExplosion = value; }
    }

    [SerializeField] ObjectPool smallExplosion;
    public ObjectPool SmallExplosion {
        get { return smallExplosion; }
        private set { smallExplosion = value; }
    }

    [SerializeField] ObjectPool tinyExplosion;
    public ObjectPool TinyExplosion {
        get { return tinyExplosion; }
        private set { tinyExplosion = value; }
    }

    [SerializeField] ObjectPool gigaExplosion;
    public ObjectPool GigaExplosion {
        get { return gigaExplosion; }
        private set { gigaExplosion = value; }
    }


    [Header("PickUps")]
    [SerializeField] CoinPool coins;
    public CoinPool Coins {
        get { return coins; }
        private set { coins = value; }
    }

    [SerializeField] PowerUpPool powerUps;
    public PowerUpPool PowerUps {
        get { return powerUps; }
        private set { powerUps = value; }
    }





    private void Reset()
    {
        GameManager = GetComponentInParent<GameManager>();
    }

    private void OnValidate()
    {
        GameManager = GetComponentInParent<GameManager>();
    }
}

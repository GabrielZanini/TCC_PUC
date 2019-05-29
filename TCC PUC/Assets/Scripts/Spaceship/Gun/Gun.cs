﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Barrels")]
    [SerializeField] GameObject barrelPrefab;
    [SerializeField] List<Barrel> barrels = new List<Barrel>();


    [Header("Barrels Setting")]
    [Range(1, 10)] [SerializeField] int maxBarrels = 10;
    public int MaxBarrels {
        get { return maxBarrels; }
        set { maxBarrels = value; AjustBarrols(); }
    }
    [SerializeField] float barrelAngle = 10f;
    public float BarrelAngle {
        get { return barrelAngle; }
        set { barrelAngle = value; RotateBarrels(); }
    }
    [SerializeField] float barrelMaxAngle = 90f;
    public float BarrelMaxAngle {
        get { return barrelMaxAngle; }
        set { barrelMaxAngle = value; RotateBarrels(); }
    }
    [SerializeField] float barrelSize = 1f;


    [Header("Muzzle Settings")]
    [SerializeField] float muzzleAngle = 0f;
    public float MuzzleAngle {
        get { return muzzleAngle; }
        set { muzzleAngle = value; CalculateMuzzlesOffset(); }
    }
    [SerializeField] float muzzlesMaxAngle = 90f;
    public float MuzzlesMaxAngle {
        get { return muzzlesMaxAngle; }
        set { muzzlesMaxAngle = value; CalculateMuzzlesOffset(); }
    }


    [Header("Bullet Settings")]
    public BulletType bulletType = BulletType.Sphere;
    public ObjectPool bulletPool;
    public int bulletDamage = 1;
    public float bulletSpeed = 30f;
    [Range(0.01f, 1f)] public float bulletRate = 0.1f;
    public Color inColor = Color.white;
    public Color outColor = Color.blue;
    public string layerName;
    int bulletLayer = 0;


    [Header("Control")]
    public bool isLocked = false;
    public bool isTriggerPulled = false;


    [Header("Audio")]
    [SerializeField] AudioManager audio;
    bool hasAudio = false;

    [Header("Difficulty")]
    public bool scaleWithDificulty = true;

    // AUx variables
    private float timer = 0f;
    TimeBody bulletTimebody;



    private void Awake()
    {
        hasAudio = audio != null;

        AjustBarrols();
    }

    private void Start()
    {
        GetBulletPool();
    }

    private void OnValidate()
    {
        AjustBarrols();
    }

    void Update()
    {
        CheckForShoot();
    }



    // Shooting

    public void PullTrigger()
    {
        isTriggerPulled = true;
    }

    public void ReleaseTrigger()
    {
        isTriggerPulled = false;
    }

    void CheckForShoot()
    {
        if (isTriggerPulled && !isLocked)
        {
            if (timer <= 0f)
            {
                if (scaleWithDificulty)
                {
                    timer = bulletRate / GameManager.Instance.Level.DifficultyModifire;
                }
                else
                {
                    timer = bulletRate;
                }
                
                Shoot();
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    void Shoot()
    {

        for (int i = 0; i < MaxBarrels; i++)
        {
            bulletTimebody = bulletPool.Spawn(barrels[i].muzzle.position, barrels[i].muzzle.rotation);

            bulletTimebody.gameObject.layer = bulletLayer;

            if (scaleWithDificulty)
            {
                bulletTimebody.bullet.speed = bulletSpeed * GameManager.Instance.Level.DifficultyModifire;
                bulletTimebody.bullet.damage = Mathf.RoundToInt(bulletDamage * GameManager.Instance.Level.DifficultyModifire);
            }
            else
            {
                bulletTimebody.bullet.speed = bulletSpeed;
                bulletTimebody.bullet.damage = bulletDamage;
            }            

            bulletTimebody.bullet.inRender.color = inColor;
            bulletTimebody.bullet.outRender.color = outColor;
            bulletTimebody.bullet.SetScale(bulletDamage * 0.5f);
        }

        if (hasAudio)
        {
            audio.Replay();
        }
    }



    // Gun Settings

    void AjustBarrols()
    {
        GetBarrel();
        CreateBarrels();
        EnableDisableBarrels();
        RotateBarrels();
        ResizeBarrel();
        CalculateMuzzlesOffset();
    }

    void GetBarrel()
    {
        barrels.Clear();

        var barr = GetComponentsInChildren<Barrel>(true);

        for (int i=0; i<barr.Length; i++)
        {
            barrels.Add(barr[i]);
        }
    }

    void CreateBarrels()
    {
        while (maxBarrels > barrels.Count)
        {
            var barrel = Instantiate(barrelPrefab, transform);
            barrels.Add(barrel.GetComponent<Barrel>());
        }
    }
    
    void EnableDisableBarrels()
    {
        for (int i=0; i<barrels.Count; i++)
        {
            if (i < maxBarrels)
            {
                barrels[i].gameObject.SetActive(true);
            }
            else
            {
                barrels[i].gameObject.SetActive(false);
            }           
        }
    }

    void RotateBarrels()
    {
        float totalAngle = (MaxBarrels - 1) * barrelAngle;
        float offsetAngles = barrelAngle;

        if (totalAngle > barrelMaxAngle) {
            totalAngle = barrelMaxAngle;
            offsetAngles = totalAngle / (MaxBarrels - 1);
        }
        
        float startAngle = totalAngle / 2f;

        for (int i=0; i<maxBarrels; i++)
        {
            barrels[i].transform.localRotation = Quaternion.Euler(0f, (startAngle - i * offsetAngles), 0f);
        }
    }

    void ResizeBarrel()
    {
        for (int i = 0; i < maxBarrels; i++)
        {
            barrels[i].muzzle.localPosition = new Vector3(0f, 0f, barrelSize);
        }
    }

    void CalculateMuzzlesOffset()
    {
        float totalAngle = (MaxBarrels - 1) * muzzleAngle;
        float offsetAngles = muzzleAngle;
        Quaternion forwardRotation = Quaternion.LookRotation(transform.forward, transform.up);

        if (totalAngle > muzzlesMaxAngle)
        {
            totalAngle = muzzlesMaxAngle;
            offsetAngles = totalAngle / (MaxBarrels - 1);
        }

        float startAngle = totalAngle / 2f;

        for (int i = 0; i < maxBarrels; i++)
        {
            barrels[i].muzzle.rotation = forwardRotation * Quaternion.Euler(0f, (startAngle - i * offsetAngles), 0f);
        }
    }

    public void SetBulletLayer(string layer)
    {
        layerName = layer;
        bulletLayer = LayerMask.NameToLayer(layer);
    }

    void GetBulletPool()
    {
        if (bulletType == BulletType.Sphere)
        {
            bulletPool = GameManager.Instance.Pools.SphereBullets;
        }
        else if (bulletType == BulletType.Egg)
        {
            bulletPool = GameManager.Instance.Pools.EggBullets;
        }
        else if (bulletType == BulletType.Drop)
        {
            bulletPool = GameManager.Instance.Pools.DropBullets;
        }
        else
        {

        }
    }


    // External Control

    public void SetBullets(int bullets)
    {
        MaxBarrels = bullets;
        AjustBarrols();
    }

    public void SetDamage(int damage)
    {
        bulletDamage = damage;
    }

    public void SetRate(float rate)
    {
        bulletRate = rate;
    }

    public void SetBulletColor(Color inColor, Color outColor)
    {
        this.inColor = inColor;
        this.outColor = outColor;
    }


    public void AddBullet()
    {
        if (MaxBarrels < 10)
        {
            MaxBarrels += 1;
            AjustBarrols();
        }
    }

    public void RemoveBullet()
    {
        if (MaxBarrels > 1)
        {
            MaxBarrels -= 1;
            AjustBarrols();
        }
    }

    public void AddDamage()
    {
        if (bulletDamage < 10)
        {
            bulletDamage += 1;
        }
    }

    public void RemoveDamage()
    {
        if (bulletDamage > 1)
        {
            bulletDamage -= 1;
        }
    }

    public void AddBarrelAngle()
    {
        BarrelAngle += 1;
        RotateBarrels();
        CalculateMuzzlesOffset();
    }

    public void RemoveBarrelAngle()
    {
        BarrelAngle -= 1;
        RotateBarrels();
        CalculateMuzzlesOffset();
    }
    
    public void AddMuzzleAngle()
    {
        MuzzleAngle += 1;
        CalculateMuzzlesOffset();
    }

    public void RemoveMuzzleAngle()
    {
        MuzzleAngle -= 1;
        CalculateMuzzlesOffset();
    }

    public void AddBulletSpeed()
    {
        bulletSpeed += 1;
    }

    public void RemoveBulletSpeed()
    {
        bulletSpeed -= 1;
    }

    public void AddBulletRate()
    {
        bulletRate += 0.02f;
    }

    public void RemoveBulletRate()
    {
        if (bulletRate > 0.01f)
        {
            bulletRate -= 0.02f;
        }        
    }
}

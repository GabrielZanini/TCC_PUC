using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Barrels")]
    [SerializeField] List<Barrel> barrels = new List<Barrel>();


    [Header("Barrels Setting")]
    [Range(1,10)][SerializeField] int maxBarrels = 10;
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

    [Header("Bullet Settings")]
    [SerializeField] float bulletAngle = 0f;
    public float BulletAngle {
        get { return bulletAngle; }
        set { bulletAngle = value; CalculateMuzzlesOffste(); }
    }
    [SerializeField] float bulletMaxAngle = 90f;
    public float BulletMaxAngle {
        get { return bulletMaxAngle; }
        set { bulletMaxAngle = value; CalculateMuzzlesOffste(); }
    }


    [Header("Bullet Spwaning")]
    public ObjectPool bulletPool;
    public int bulletdamage = 1;
    public float bulletSpeed = 30f;
    [Range(0.01f, 1f)] public float bulletRate = 0.1f;
    public bool autoShoot = true;
    public bool canShoot = true;

    [Header("Audio")]
    [SerializeField] AudioManager audio;
    bool hasAudio = false;

    // AUx variables
    private float timer = 0f;
    Quaternion forwardRotation;
    TimeBody timebody;
    Bullet bullet;


    private void Awake()
    {
        hasAudio = audio != null;

        AjustBarrols();
    }

    private void OnValidate()
    {
        AjustBarrols();
    }


    void Update()
    {
        if (GameManager.Instance.Level.IsPlaying)
        {
            if (autoShoot)
            {
                AutoShoot();
            }
            else
            {
                ManualShoot();
            }
        }
    }

    void AutoShoot()
    {
        if (timer <= 0f)
        {
            timer = bulletRate;
            Shoot();
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    void ManualShoot()
    {
        if (timer <= 0f)
        {
            if (Input.GetButton("Jump"))
            {
                timer = bulletRate;

                Shoot();
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            timer = 0f;
        }
    }

    void Shoot()
    {
        forwardRotation = Quaternion.LookRotation(transform.forward);

        for (int i = 0; i < MaxBarrels; i++)
        {
            timebody = bulletPool.Spawn(barrels[i].muzzle.position, forwardRotation * barrels[i].offset);
            bullet = timebody.GetComponent<Bullet>();

            bullet.speed = bulletSpeed;
            bullet.damage = bulletdamage;
        }

        if (hasAudio)
        {
            audio.Replay();
        }
    }



    void AjustBarrols()
    {
        EnableDisableBarrels();
        RotateBarrels();
        CalculateMuzzlesOffste();
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
            barrels[i].transform.rotation = Quaternion.Euler(0f, (startAngle - i * offsetAngles), 0f);
        }
    }

    void CalculateMuzzlesOffste()
    {
        float totalAngle = (MaxBarrels - 1) * bulletAngle;
        float offsetAngles = bulletAngle;

        if (totalAngle > bulletMaxAngle)
        {
            totalAngle = bulletMaxAngle;
            offsetAngles = totalAngle / (MaxBarrels - 1);
        }

        float startAngle = totalAngle / 2f;

        for (int i = 0; i < maxBarrels; i++)
        {
            barrels[i].offset = Quaternion.Euler(0f, (startAngle - i * offsetAngles), 0f);
        }
    }



    // External Control

    public void MoreBullet()
    {
        MaxBarrels += 1;
    }

    public void LessBullet()
    {
        MaxBarrels -= 1;
    }
    
    public void MoreBarrelAngle()
    {
        BarrelAngle += 1;
    }

    public void LessBarrelAngle()
    {
        BarrelAngle -= 1;
    }
    
    public void MoreBulletAngle()
    {
        BulletAngle += 1;
    }

    public void LessBulletAngle()
    {
        BulletAngle -= 1;
    }

    public void MoreBulletSpeed()
    {
        bulletSpeed += 1;
    }

    public void LessBulletSpeed()
    {
        bulletSpeed -= 1;
    }

    public void MoreBulletRate()
    {
        bulletRate += 0.1f;
    }

    public void LessBulletRate()
    {
        bulletRate -= 0.1f;
    }
}

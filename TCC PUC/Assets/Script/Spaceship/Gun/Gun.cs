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

    [Header("Muzzle Settings")]
    [SerializeField] float muzzleAngle = 0f;
    public float MuzzleAngle {
        get { return muzzleAngle; }
        set { muzzleAngle = value; CalculateMuzzlesOffste(); }
    }
    [SerializeField] float muzzlesMaxAngle = 90f;
    public float MuzzlesMaxAngle {
        get { return muzzlesMaxAngle; }
        set { muzzlesMaxAngle = value; CalculateMuzzlesOffste(); }
    }


    [Header("Bullet Settings")]
    public ObjectPool bulletPool;
    public int bulletdamage = 1;
    public float bulletSpeed = 30f;
    [Range(0.01f, 1f)] public float bulletRate = 0.1f;
    public Color inColor = Color.white;
    public Color outColor = Color.blue;


    [Header("Control")]
    public bool isLocked = false;
    public bool isTriggerPulled = false;


    [Header("Audio")]
    [SerializeField] AudioManager audio;
    bool hasAudio = false;

    // AUx variables
    private float timer = 0f;
    Quaternion forwardRotation;
    TimeBody bulletTimebody;


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
        if (isTriggerPulled && !isLocked)
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
    }

    public void PullTrigger()
    {
        isTriggerPulled = true;
    }
    
    public void ReleaseTrigger()
    {
        isTriggerPulled = false;
    }



    void Shoot()
    {
        forwardRotation = Quaternion.LookRotation(transform.forward);

        for (int i = 0; i < MaxBarrels; i++)
        {
            bulletTimebody = bulletPool.Spawn(barrels[i].muzzle.position, forwardRotation * barrels[i].offset);

            bulletTimebody.bullet.speed = bulletSpeed;
            bulletTimebody.bullet.damage = bulletdamage;

            bulletTimebody.bullet.inRender.color = inColor;
            bulletTimebody.bullet.outRender.color = outColor;
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
        float totalAngle = (MaxBarrels - 1) * muzzleAngle;
        float offsetAngles = muzzleAngle;

        if (totalAngle > muzzlesMaxAngle)
        {
            totalAngle = muzzlesMaxAngle;
            offsetAngles = totalAngle / (MaxBarrels - 1);
        }

        float startAngle = totalAngle / 2f;

        for (int i = 0; i < maxBarrels; i++)
        {
            barrels[i].offset = Quaternion.Euler(0f, (startAngle - i * offsetAngles), 0f);
        }
    }



    // External Control

    public void MoreBullets()
    {
        if (MaxBarrels < 10)
        {
            MaxBarrels += 1;
            AjustBarrols();
        }        
    }

    public void LessBullets()
    {
        if (MaxBarrels > 1)
        {
            MaxBarrels -= 1;
            AjustBarrols();
        }
    }
    
    public void MoreBarrelAngle()
    {
        BarrelAngle += 1;
        RotateBarrels();

    }

    public void LessBarrelAngle()
    {
        BarrelAngle -= 1;
        RotateBarrels();
    }
    
    public void MoreMuzzleAngle()
    {
        MuzzleAngle += 1;
        CalculateMuzzlesOffste();
    }

    public void LessMuzzleAngle()
    {
        MuzzleAngle -= 1;
        CalculateMuzzlesOffste();
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
        bulletRate += 0.01f;
    }

    public void LessBulletRate()
    {
        if (bulletRate > 0.01f)
        {
            bulletRate -= 0.01f;
        }        
    }
}

using System.Collections;
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
        set { muzzleAngle = value; CalculateMuzzlesOffste(); }
    }
    [SerializeField] float muzzlesMaxAngle = 90f;
    public float MuzzlesMaxAngle {
        get { return muzzlesMaxAngle; }
        set { muzzlesMaxAngle = value; CalculateMuzzlesOffste(); }
    }


    [Header("Bullet Settings")]
    public BulletType bulletType = BulletType.Sphere;
    public ObjectPool bulletPool;
    public int bulletdamage = 1;
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
                timer = bulletRate;
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



    // Gun Settings

    void AjustBarrols()
    {
        GetBarrel();
        CreateBarrels();
        EnableDisableBarrels();
        RotateBarrels();
        ResizeBarrel();
        CalculateMuzzlesOffste();
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

    void CalculateMuzzlesOffste()
    {
        float totalAngle = (MaxBarrels - 1) * muzzleAngle;
        float offsetAngles = muzzleAngle;
        Quaternion forwardRotation = Quaternion.LookRotation(transform.forward);

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
        CalculateMuzzlesOffste();
    }

    public void LessBarrelAngle()
    {
        BarrelAngle -= 1;
        RotateBarrels();
        CalculateMuzzlesOffste();
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

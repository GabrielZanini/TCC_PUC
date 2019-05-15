using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootShip : MonoBehaviour
{
    public Transform gunHole;
    public LayerMask targetLayers;


    [Range(1, 10)] public uint bulletsPerShoot = 1;
    [Range(1, 15)] public float bulletAngle = 5;
    public bool autoShoot = false;
    
    private float timer = 0f;

    StatusShip status;
    RaycastHit hit;
    bool canShoot;
    float totalArc;
    float startAngle;

    void Start()
    {
        status = GetComponent<StatusShip>();
    }

    void Update()
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


    void AutoShoot()
    {
        CastRay();

        if (canShoot)
        {
            if (timer <= 0f)
            {
                timer = status.shootingRate;
                Shoot();
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    void ManualShoot()
    {
        if (timer <= 0f)
        {
            if (Input.GetButton("Jump"))
            {
                timer = status.shootingRate;

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
        for (int i = 0; i < bulletsPerShoot; i++)
        {
            var bulletObject = BulletPool.Instance.Spawn(gunHole.position, gunHole.rotation * Quaternion.Euler((startAngle - i * bulletAngle), 0,0));

            if (bulletObject != null)
            {
                bulletObject.GetComponent<Bullet>().speed = status.shootingSpeed;
            }
        }
    }

    void CastRay()
    {
        totalArc = (bulletsPerShoot - 1) * bulletAngle;
        startAngle = totalArc / 2f;

        // DEBUG
        for (int i = 0; i < bulletsPerShoot; i++)
        {
            //Debug.Log("I: " + i + " - Angle: " + (startAngle + i * bulletAngle));
            Debug.DrawRay(gunHole.position, Quaternion.Euler(0, 0, (startAngle - i * bulletAngle)) * gunHole.forward * 1000, Color.yellow);
        }

        Debug.DrawRay(gunHole.position, (Quaternion.Euler(0, 0, (startAngle + bulletAngle)) * gunHole.forward) * 1000, Color.green);
        Debug.DrawRay(gunHole.position, (Quaternion.Euler(0, 0, (startAngle - bulletsPerShoot * bulletAngle)) * gunHole.forward) * 1000, Color.green);

        // Cast
        for (int i = -1; i < bulletsPerShoot + 1; i++)
        {
            canShoot = Physics.Raycast(gunHole.position, Quaternion.Euler(0, 0, (startAngle - i * bulletAngle)) * gunHole.forward, out hit, Mathf.Infinity, targetLayers);
            if (canShoot) break;
        }
    }


    
     
    public void MoreBullets()
    {
        if (bulletsPerShoot < 10)
        {
            bulletsPerShoot++;
        }
    }

    public void LessBullets()
    {
        if (bulletsPerShoot > 1)
        {
            bulletsPerShoot--;
        }
    }

    public void MoreAngle()
    {
        if (bulletAngle < 15)
        {
            bulletAngle++;
        }
    }

    public void LessAngle()
    {
        if (bulletAngle > 1)
        {
            bulletAngle--;
        } 
    }
}

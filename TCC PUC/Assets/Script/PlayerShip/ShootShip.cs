using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootShip : MonoBehaviour
{
    public Transform gun;
    public Transform gunHole;
    public LayerMask targetLayers;


    [Range(1, 10)] public uint bulletsPerShoot = 1;
    [Range(0, 15)] public float bulletAngle = 5;
    [Range(0, 15)] public float bulletDistance = 1f;
    public bool autoShoot = false;
    
    private float timer = 0f;

    StatusShip status;
    RaycastHit hit;
    bool canShoot;

    float totalArc;
    float startAngle;

    float totalDistance;
    float startDistance;

    void Start()
    {
        status = GetComponent<StatusShip>();
    }

    void Update()
    {
        CastRay();

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
            gun.localRotation = Quaternion.Euler(0, (startDistance - i * bulletDistance), 0);

            var bulletTimebody = BulletPool.Instance.Spawn(gunHole.position, Quaternion.Euler(0, (startAngle - i * bulletAngle), 0));


            if (bulletTimebody != null)
            {
                bulletTimebody.GetComponent<Bullet>().speed = status.shootingSpeed;
            }
        }
    }

    void CastRay()
    {
        totalDistance = (bulletsPerShoot - 1) * bulletDistance;
        startDistance = totalDistance / 2f;

        totalArc = (bulletsPerShoot - 1) * bulletAngle;
        startAngle = totalArc / 2f;

        // DEBUG
        for (int i = 0; i < bulletsPerShoot; i++)
        {
            gun.localRotation = Quaternion.Euler((startDistance - i * bulletDistance), 0, 0);

            //Debug.Log("I: " + i + " - Angle: " + (startAngle + i * bulletAngle));
            Debug.DrawRay(gunHole.position, Quaternion.Euler(0, 0, (startAngle - i * bulletAngle)) * Vector3.forward * 1000, Color.yellow);
        }

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
        if (bulletAngle > 0)
        {
            bulletAngle--;
        } 
    }
    
    public void MoreDistance()
    {
        if (bulletDistance < 15)
        {
            bulletDistance++;
        }
    }

    public void LessDistance()
    {
        if (bulletDistance > 0)
        {
            bulletDistance--;
        }
    }
}

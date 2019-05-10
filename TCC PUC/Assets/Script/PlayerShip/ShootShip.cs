using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootShip : MonoBehaviour
{
    public Transform gunHole;

    public uint BulletsPerShoot = 1;

    private float timer = 0f;

    StatusShip status;

    void Start()
    {
        status = GetComponent<StatusShip>();
    }

    void Update()
    {
        if (TimeController.Instance.isRewinding)
        {
            return;
        }

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
        //var bullet = Instantiate(projectile, gunHole.position, gunHole.rotation);
        var bullet = BulletPool.Instance.SpawnBullet(gunHole.position, gunHole.rotation);
    }
}

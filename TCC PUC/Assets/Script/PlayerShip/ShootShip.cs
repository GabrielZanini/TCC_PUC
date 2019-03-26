using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootShip : MonoBehaviour
{
    public GameObject projectile;
    public Transform gunHole;
    public float rate = 0.5f;

    private float timer = 0f;

    TimeBody timeBody;

    void Start()
    {
        timeBody = GetComponent<TimeBody>();
    }

    void Update()
    {
        if (timeBody.isRewinding)
        {
            return;
        }

        if (timer <= 0f)
        {
            if (Input.GetButton("Jump"))
            {
                timer = rate;

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
        var bullet = Instantiate(projectile, gunHole.position, gunHole.rotation);
    }
}

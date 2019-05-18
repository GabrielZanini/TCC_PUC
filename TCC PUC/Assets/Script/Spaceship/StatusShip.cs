using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusShip : StatusBase
{
    [Header("Angular Speed")]
    public float angularSpeed = 1f;
    public float slowdownModifier = 1.5f;


    [Header("Shooting")]
    public float shootingRate = 0.5f;
    public float shootingSpeed = 10f;



    public void MoreShootingRate()
    {
        shootingRate += 0.01f;
    }

    public void LessShootingRate()
    {
        if (shootingRate > 0.01f)
        {
            shootingRate -= 0.01f;
        }
    }



    public void MoreShootingSpeed()
    {
        shootingSpeed++;
    }

    public void LessShootingSpeed()
    {
        if (shootingSpeed > 1f)
        {
            shootingSpeed--;
        }
    }
}

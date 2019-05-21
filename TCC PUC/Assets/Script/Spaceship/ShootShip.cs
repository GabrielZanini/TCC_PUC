using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootShip : MonoBehaviour
{
    public List<Gun> guns = new List<Gun>();

    private void Start()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].gameObject.SetActive(true);
        }
    }


    public void MoreBullet()
    {
        for (int i=0; i<guns.Count; i++)
        {
            guns[i].MoreBullet();
        }
    }

    public void LessBullet()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].LessBullet();
        }
    }

    public void MoreBarrelAngle()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].MoreBarrelAngle();
        }
    }

    public void LessBarrelAngle()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].LessBarrelAngle();
        }
    }

    public void MoreBulletAngle()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].MoreBulletAngle();
        }
    }

    public void LessBulletAngle()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].LessBulletAngle();
        }
    }

    public void MoreBulletSpeed()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].MoreBulletSpeed();
        }
    }

    public void LessBulletSpeed()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].LessBulletSpeed();
        }
    }

    public void MoreBulletRate()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].MoreBulletRate();
        }
    }

    public void LessBulletRate()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].LessBulletRate();
        }
    }

}

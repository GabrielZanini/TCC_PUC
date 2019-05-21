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
    
        

    public void PullTriggers()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].PullTrigger();
        }
    }

    public void ReleaseTriggers()
    {        
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].ReleaseTrigger();
        }
    }



    public void MoreBullets()
    {
        for (int i=0; i<guns.Count; i++)
        {
            guns[i].MoreBullets();
        }
    }

    public void LessBullets()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].LessBullets();
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

    public void MoreMuzzleAngle()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].MoreMuzzleAngle();
        }
    }

    public void LessMuzzleAngle()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].LessMuzzleAngle();
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

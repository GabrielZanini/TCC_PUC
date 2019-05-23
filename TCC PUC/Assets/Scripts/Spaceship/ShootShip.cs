using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootShip : MonoBehaviour
{
    public List<Gun> guns = new List<Gun>();
    public bool triggersPulled = false;


    private void Reset()
    {
        GetGuns();
    }
    
    private void Awake()
    {        
        ActivateGuns();
    }

    private void OnEnable()
    {
        ActivateGuns();
    }

    private void OnDisable()
    {
        DeactivateGuns();
    }

    private void OnValidate()
    {
        GetGuns();
    }



    void GetGuns()
    {
        var g = GetComponentsInChildren<Gun>(true);

        guns.Clear();

        for (int i=0; i<g.Length; i++)
        {
            guns.Add(g[i]);
        }
    }

    void ActivateGuns()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].gameObject.SetActive(true);
        }
    }

    void DeactivateGuns()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].gameObject.SetActive(false);
        }
    }


    public void SetBulletsLayer(string layer)
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].SetBulletLayer(layer);
        }
    }



    public void PullTriggers()
    {
        triggersPulled = true;

        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].PullTrigger();
        }
    }

    public void ReleaseTriggers()
    {
        triggersPulled = false;

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

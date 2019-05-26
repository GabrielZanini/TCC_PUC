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



    public void SetBullets(int bullets)
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].SetBullets(bullets);
        }
    }


    public void AddBullet()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].AddBullet();
        }
    }

    public void RemoveBullet()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].RemoveBullet();
        }
    }

    public void AddDamage()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].AddDamage();
        }
    }

    public void RemoveDamage()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].RemoveDamage();
        }
    }

    public void AddBarrelAngle()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].AddBarrelAngle();
        }
    }

    public void RemoveBarrelAngle()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].RemoveBarrelAngle();
        }
    }

    public void AddMuzzleAngle()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].AddMuzzleAngle();
        }
    }

    public void RemoveMuzzleAngle()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].RemoveMuzzleAngle();
        }
    }

    public void AddBulletSpeed()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].AddBulletSpeed();
        }
    }

    public void RemoveBulletSpeed()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].RemoveBulletSpeed();
        }
    }

    public void AddBulletRate()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].AddBulletRate();
        }
    }

    public void RemoveBulletRate()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].RemoveBulletRate();
        }
    }

}

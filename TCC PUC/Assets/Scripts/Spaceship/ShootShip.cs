using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootShip : MonoBehaviour
{
    [Header("Guns")]
    public List<Gun> guns = new List<Gun>();
    
    [Header("Status")]
    public bool triggersPulled = false;
    
    [Header("Difficulty")]
    public bool scaleWithDificulty = true;


    private void Reset()
    {
        GetGuns();
    }

    private void OnValidate()
    {
        GetGuns();
    }

    private void Awake()
    {        
        ActivateGuns();
    }

    private void OnEnable()
    {
        //ActivateGuns();
    }

    private void OnDestroy()
    {
        DeactivateGuns();
    }



    void GetGuns()
    {
        var g = GetComponentsInChildren<Gun>(true);

        guns.Clear();

        for (int i=0; i<g.Length; i++)
        {
            guns.Add(g[i]);
        }

        SetScaleWithDifficulty();
    }

    public void ActivateGuns()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].gameObject.SetActive(true);
        }
    }

    public void DeactivateGuns()
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



    public void SetScaleWithDifficulty()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].scaleWithDificulty = scaleWithDificulty;
        }
    }

    public void SetBullets(int bullets)
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].SetBullets(bullets);
        }
    }

    public void SetDamage(int damage)
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].SetDamage(damage);
        }
    }

    public void SetRate(float rate)
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].SetRate(rate);
        }
    }

    public void SetBulletColor(Color inColor, Color outColor)
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].SetBulletColor(inColor, outColor);
        }
    }

    public void SetTarget(Transform t)
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].SetTarget(t);
        }
    }



    public void AddBullet(int bullets = 1)
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].AddBullet(bullets);
        }
    }

    public void RemoveBullet()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].RemoveBullet();
        }
    }

    public void AddDamage(int damage = 1)
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].AddDamage(damage);
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

    public void AddBulletRate(float rate = 0.02f)
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].AddBulletRate(rate);
        }
    }

    public void RemoveBulletRate(float rate = 0.02f)
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].RemoveBulletRate(rate);
        }
    }

}

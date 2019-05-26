using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsteroidManager : ObjectManager
{
    [Header("Settings")]
    public int points = 10;
    public float lifeTime = 10f;
    float despawnCounter = 0f;



    protected override void Reset()
    {
        base.Reset();
    }

    private void OnEnable()
    {
        despawnCounter = lifeTime;
    }

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        Count();
    }


    protected override void Death()
    {
        base.Death();
        GameManager.Instance.Level.Score.Add(points);
    }

    void Count()
    {
        if (despawnCounter <= 0f)
        {
            timebody.Despawn();
        }
        else
        {
            despawnCounter -= Time.deltaTime;
        }
    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimeBody))]
[RequireComponent(typeof(StatusBase))]
public class ObjectManager : MonoBehaviour
{
    public TimeBody timebody;
    public StatusBase status;


    protected virtual void Reset()
    {
        timebody = GetComponent<TimeBody>();
        timebody.scriptsToDisable.Add(this);
        status = GetComponent<StatusBase>();
    }
    
    protected virtual void Start()
    {
        AddListeners();
    }
    
    protected virtual void OnDestroy()
    {
        RemoveListeners();
    }



    // Listeners

    protected virtual void AddListeners()
    {
        status.OnDeath.AddListener(Death);
    }

    protected virtual void RemoveListeners()
    {
        status.OnDeath.RemoveListener(Death);
    }



    // Collision - Trigger

    private void OnTriggerEnter(Collider other)
    {
        var otherStatus = other.gameObject.GetComponent<StatusBase>();

        if (otherStatus != null)
        {
            int otherHp = otherStatus.CurrentHp;
            otherStatus.TakeDamage(status.CurrentHp);
            status.TakeDamage(otherHp);
        }
    }


    // Event

    protected virtual void Death()
    {
        SpawnEffect();        
        timebody.Despawn();
    }

    public void SpawnEffect()
    {
        if (status.deathEffect == Enums.EffectType.BigExplosion)
        {
            GameManager.Instance.Pools.BigExplosion.Spawn(transform.position);
        }
        else if (status.deathEffect == Enums.EffectType.DustExplosion)
        {
            GameManager.Instance.Pools.DustExplosion.Spawn(transform.position);
        }
        else if (status.deathEffect == Enums.EffectType.SmallExplosion)
        {
            GameManager.Instance.Pools.SmallExplosion.Spawn(transform.position);
        }
        else if (status.deathEffect == Enums.EffectType.TinyExplosion)
        {
            GameManager.Instance.Pools.TinyExplosion.Spawn(transform.position);
        }
        else
        {

        }
    }
}

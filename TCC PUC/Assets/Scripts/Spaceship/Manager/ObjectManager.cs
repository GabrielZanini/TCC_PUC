using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimeBody))]
[RequireComponent(typeof(HeathShip))]
public class ObjectManager : MonoBehaviour
{
    [Header("Scripts")]
    public TimeBody timebody;
    public HeathShip health;


    protected virtual void Reset()
    {
        timebody = GetComponent<TimeBody>();

        if (!timebody.scriptsToDisable.Contains(this))
        {
            timebody.scriptsToDisable.Add(this);
        }

        health = GetComponent<HeathShip>();
    }

    protected virtual void OnValidate()
    {
        Reset();
    }

    protected virtual void Start()
    {
        Reset();
        AddListeners();
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void OnDestroy()
    {
        RemoveListeners();
    }



    // Listeners

    protected virtual void AddListeners()
    {
        health.OnDeath.AddListener(Death);
    }

    protected virtual void RemoveListeners()
    {
        health.OnDeath.RemoveListener(Death);
    }



    // Collision - Trigger

    private void OnTriggerEnter(Collider other)
    {
        var otherHealth = other.gameObject.GetComponent<HeathShip>();

        if (otherHealth != null)
        {
            int otherHp = otherHealth.CurrentHp;
            otherHealth.TakeDamage(health.CurrentHp);
            health.TakeDamage(otherHp);
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
        if (health.deathEffect == EffectType.BigExplosion)
        {
            GameManager.Instance.Pools.BigExplosion.Spawn(transform.position);
        }
        else if (health.deathEffect == EffectType.DustExplosion)
        {
            GameManager.Instance.Pools.DustExplosion.Spawn(transform.position);
        }
        else if (health.deathEffect == EffectType.SmallExplosion)
        {
            GameManager.Instance.Pools.SmallExplosion.Spawn(transform.position);
        }
        else if (health.deathEffect == EffectType.TinyExplosion)
        {
            GameManager.Instance.Pools.TinyExplosion.Spawn(transform.position);
        }
        else
        {

        }
    }
}

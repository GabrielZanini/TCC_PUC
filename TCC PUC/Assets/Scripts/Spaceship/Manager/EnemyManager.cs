using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShipAi))]
public class EnemyManager : ShipManager
{
    [Header("Enemy Settings")]

    float despawnCounter = 0f;

    public EnemyStatus enemyStatus;


    protected override void Reset()
    {
        base.Reset();

        type = ShipType.Enemy;

        if (enemyStatus != null)
        {
            shipStatus = enemyStatus;
        }
    }


    private void OnEnable()
    {
        //StartEnemy();
    }

    protected override void Start()
    {
        base.Start();
        StartEnemy();
    }

    protected override void Update()
    {
        base.Update();
        DespawnCount();
    }


    protected override void AddListeners()
    {
        base.AddListeners();
        timebody.OnActivate.AddListener(StartEnemy);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        timebody.OnActivate.RemoveListener(StartEnemy);
    }



    IEnumerator DeactivateEnemy()
    {
        timebody.DisableCollider();
        shoot.DeactivateGuns();

        yield return new WaitForSeconds(enemyStatus.activateTime); 

        timebody.EnableCollider();
        shoot.ActivateGuns();
    }

    void StartEnemy()
    {
        despawnCounter = enemyStatus.lifeTime;

        if (enemyStatus.targetPlayer)
        {
            shoot.SetTarget(timebody.Controller.GameManager.Player.transform);
        }

        //StartCoroutine(DeactivateEnemy());
    }


    void DespawnCount()
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



    protected override void Death()
    {
        base.Death();
        GameManager.Instance.Level.Score.Add(enemyStatus.points);
        GameManager.Instance.Pools.PowerUps.TrySpawn(transform.position);        
    }
}

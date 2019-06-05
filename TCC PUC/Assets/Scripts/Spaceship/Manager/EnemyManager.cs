using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShipAi))]
public class EnemyManager : ShipManager
{
    [Header("Enemy Settings")]
    public int points = 10;
    public float activateTime = 4f;
    public float lifeTime = 10f;
    float despawnCounter = 0f;
    public bool targetPlayer = false;

    public EnemyStatus defaultEnemyStatus;


    protected override void Reset()
    {
        base.Reset();

        type = ShipType.Enemy;
        playAfterStop = true;

        if (defaultEnemyStatus != null)
        {
            defaultShipStatus = defaultEnemyStatus;
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

        yield return new WaitForSeconds(activateTime); 

        timebody.EnableCollider();
        shoot.ActivateGuns();
    }

    void StartEnemy()
    {
        despawnCounter = lifeTime;

        if (targetPlayer)
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
        GameManager.Instance.Level.Score.Add(points);
        GameManager.Instance.Pools.PowerUps.TrySpawn(transform.position);        
    }
}

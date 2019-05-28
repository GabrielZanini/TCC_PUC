using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : ShipManager
{
    [Header("Enemy Settings")]
    public int points = 10;
    public float activateTime = 4f;


    protected override void Reset()
    {
        base.Reset();
        type = ShipType.Enemy;
        playAfterStop = true;
    }

    private void OnEnable()
    {
        StartEnemy();
    }

    protected override void Start()
    {
        base.Start();
        StartEnemy();
    }

    protected override void Update()
    {
        base.Update();
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
        StartCoroutine(DeactivateEnemy());
    }
    


    protected override void Death()
    {
        base.Death();
        GameManager.Instance.Level.Score.Add(points);
        GameManager.Instance.Pools.PowerUps.TrySpawn(transform.position);        
    }
}

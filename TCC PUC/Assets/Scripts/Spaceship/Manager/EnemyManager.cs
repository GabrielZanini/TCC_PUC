using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : ShipManager
{
    [Header("Enemy Settings")]
    public int points = 10;


    protected override void Reset()
    {
        base.Reset();
        type = ShipType.Enemy;
        playAfterStop = true;
    }

    protected override void Death()
    {
        base.Death();
        GameManager.Instance.Score.Add(points);
    }
}

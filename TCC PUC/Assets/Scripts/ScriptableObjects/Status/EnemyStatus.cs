using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Status", menuName = "Spaceship/Status/Enemy")]
public class EnemyStatus : ShipStatus
{
    [Header("Score")]
    public int points = 10;
    
    [Header("Life")]
    public float activateTime = 2f;
    public float lifeTime = 10f;

    [Header("Behaviour")]
    public bool targetPlayer = false;




    public override void Clear()
    {
        base.Clear();

        points = 0;

        activateTime = 0f;
        lifeTime = 0f;

        targetPlayer = false;
    }

}

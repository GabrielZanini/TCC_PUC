using System.Collections;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Status", menuName = "Spaceship/Status/Player")]
public class PlayerStatus : ShipStatus
{

    [Header("Shiels")]
    public bool useShield = false;
    public float shildTime = 5f;

    [Header("Rockets")]
    public int  rockets = 1;
    public bool useRockets = false;
    public float rocketsRate = 5f;




    public override void Clear()
    {
        base.Clear();

        useShield = false;
        shildTime = 0f;

        rockets = 0;
        useRockets = false;
        rocketsRate = 0f;
    }

}


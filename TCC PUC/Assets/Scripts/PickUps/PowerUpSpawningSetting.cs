using System;
using UnityEngine;

[Serializable]
public class PowerUpSpawningSetting
{
    public PowerUpType type = PowerUpType.MoreBullets;
    public Color color = Color.yellow;
    public int weight = 1;
    public int collected = 0;
}

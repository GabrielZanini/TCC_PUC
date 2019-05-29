using System;
using UnityEngine;

[Serializable]
public class PowerUpSpawningSetting
{
    public PowerUpType type = PowerUpType.MoreBullets;
    public Sprite sprite;
    public Color color = Color.yellow;
    public int max = 1;
}

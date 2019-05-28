using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : PickUp
{
    public PowerUpType type = PowerUpType.MoreBullets;
    public SpriteRenderer render;
    


    protected override void PlayerPickUp(PlayerManager player)
    {
        if (type == PowerUpType.MoreBullets)
        {
            player.shoot.AddBullet();
        }
        else if (type == PowerUpType.MoreDamage)
        {
            player.shoot.AddDamage();

        }
        else if (type == PowerUpType.MoreShootingRate)
        {
            player.shoot.RemoveBulletRate();
        }
        else if (type == PowerUpType.Rockets)
        {

        }
        else if (type == PowerUpType.Shield)
        {
            player.shield.Activate();
        }
        else if (type == PowerUpType.Heal)
        {
            player.status.Heal(20);
        }
    }


    public void SetColor(Color color)
    {
        render.color = color;
    }
}

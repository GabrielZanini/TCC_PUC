using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : PickUp
{
    public PowerUpType type = PowerUpType.MoreBullets;
    public SpriteRenderer render;



    public int bultet = 1;
    public int damage = 2;
    public float rate = 0.02f;
    public int heal = 25;


    protected override void PlayerPickUp(PlayerManager player)
    {
        if (type == PowerUpType.MoreBullets)
        {
            player.shoot.AddBullet(bultet);
        }
        else if (type == PowerUpType.MoreDamage)
        {
            player.shoot.AddDamage(damage);

        }
        else if (type == PowerUpType.MoreShootingRate)
        {
            player.shoot.RemoveBulletRate(rate);
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
            player.health.Heal(heal);
        }

        pool.RemovePowerUpByType(type);
    }


    public void SetColor(Color color)
    {
        render.color = color;
    }

    public void SetSprite(Sprite sprite)
    {
        render.sprite = sprite;
    }
}

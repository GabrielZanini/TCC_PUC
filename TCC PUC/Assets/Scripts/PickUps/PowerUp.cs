using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : PickUp
{
    public PowerUpType type = PowerUpType.MoreBullets;


    private void OnEnable()
    {
        //type = (PowerUpType)Random.Range(0,1);
    }


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

        }
    }
}

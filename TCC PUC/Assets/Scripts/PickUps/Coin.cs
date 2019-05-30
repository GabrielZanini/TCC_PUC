﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PickUp
{
    int coins = 1;

    protected override void PlayerPickUp(PlayerManager player)
    {
        player.AddCoins(coins);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAi : ShipInput
{
    public AiType type = AiType.EnemyShip;
    


    private void Start()
    {
        if (type == AiType.DeathCross)
        {
            rotationAxis.SetFixValue(1);
            verticalAxis.SetFixValue(-1);
        }

        if (autoShoot)
        {
            shootButton.SetFixValue(true, false, false);
        }
    }

    private void Update()
    {

    }
}

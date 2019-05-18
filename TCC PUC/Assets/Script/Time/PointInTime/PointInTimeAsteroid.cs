using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInTimeAsteroid : PointInTime
{
    public int hp;


    public override void Load()
    {
        base.Load();

        if (isActive)
        {
            timebody.status.CurrentHp = hp;
        }       
    }

    public override void Save()
    {
        base.Save();

        if (isActive)
        {
            hp = timebody.status.CurrentHp;
        }        
    }
}

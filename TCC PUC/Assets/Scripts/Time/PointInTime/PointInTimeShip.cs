using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInTimeShip : PointInTime
{
    public Quaternion rotation;
    public int hp;


    public override void Load()
    {
        base.Load();

        if (isActive)
        {
            timebody.transform.rotation = rotation;
            timebody.heath.CurrentHp = hp;
        }        
    }

    public override void Save()
    {
        base.Save();

        if (isActive)
        {
            rotation = timebody.transform.rotation;
            hp = timebody.heath.CurrentHp;
        }        
    }
}

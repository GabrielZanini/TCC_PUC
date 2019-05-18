using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInTimeBackground : PointInTime
{
    public float scroll;

    public override void Load()
    {
        base.Load();


        if (isActive)
        {
            timebody.scroll.counter = scroll;
        }
        
    }

    public override void Save()
    {
        base.Save();

        if (isActive)
        {
            scroll = timebody.scroll.counter;
        }
    }
}

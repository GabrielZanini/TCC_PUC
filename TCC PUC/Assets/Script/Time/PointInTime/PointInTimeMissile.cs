using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInTimeMissile : PointInTime
{
    public Quaternion rotation;



    public override void Load()
    {
        base.Load();

        if (isActive)
        {
            timebody.transform.rotation = rotation;
        }
    }

    public override void Save()
    {
        base.Save();

        if (isActive)
        {
            rotation = timebody.transform.rotation;
        }        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInTimeParticle : PointInTime
{
    float time;

    public override void Load()
    {
        base.Load();

        if (isActive)
        {
            timebody.particle.Simulate(time);
        }
    }

    public override void Save()
    {
        base.Save();

        if (isActive)
        {
            time = timebody.particle.time;
        }
    }
}

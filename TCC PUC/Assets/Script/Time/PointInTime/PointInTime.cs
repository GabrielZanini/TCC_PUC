using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInTime
{
    public TimeBody timebody;

    public bool isActive;
    public Vector3 position;
    


    public virtual void Load()
    {
        timebody.isActive = isActive;

        if (isActive)
        {
            timebody.EnableObjects();

            if (timebody.smoothPosition)
            {
                timebody.targetPosition = position;
            }
            else
            {
                timebody.transform.position = position;
            }            
        }
        else
        {
            timebody.DisableObjects();
        }       
    }

    public virtual void Save()
    {
        isActive = timebody.isActive;

        if (isActive)
        {
            position = timebody.transform.position;
        }
    }
}

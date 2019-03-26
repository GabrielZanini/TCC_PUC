using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInTime
{
    public Vector3 position;
    public Quaternion rotation;

    public PointInTime(Vector3 _position, Quaternion _rotation)
    {
        this.position = _position;
        this.rotation = _rotation;
    }
}

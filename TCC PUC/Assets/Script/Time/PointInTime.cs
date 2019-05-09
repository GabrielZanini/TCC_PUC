using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInTime
{
    public Vector3 position;
    public Quaternion rotation;
    public bool isActive;
    public int hp;

    public PointInTime(Vector3 _position, Quaternion _rotation, bool _isActive, int _hp)
    {
        this.position = _position;
        this.rotation = _rotation;
        this.isActive = _isActive;
        this.hp = _hp;
    }
}

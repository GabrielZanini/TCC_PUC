using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPool : ObjectPool
{
    public static AsteroidPool Instance { get; private set; }
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}

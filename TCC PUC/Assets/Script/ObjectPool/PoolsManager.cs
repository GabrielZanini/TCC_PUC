using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsManager : MonoBehaviour
{
    [SerializeField] ObjectPool asteroids;
    public ObjectPool Asteroids {
        get { return asteroids; }
        private set { asteroids = value; }
    }

    [SerializeField] ObjectPool bullets;
    public ObjectPool Bullets {
        get { return bullets; }
        private set { bullets = value; }
    }
}

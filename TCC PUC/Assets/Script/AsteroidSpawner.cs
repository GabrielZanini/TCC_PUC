using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] ObjectPool pool;

    public Vector2 spawningRate;
    public Vector2 AxisRange;

    float nextSpawn = 0f;
    float nextX = 0f;

    
    Vector3 position;


    void Awake()
    {
        if (pool == null)
        {
            pool = GetComponent<AsteroidPool>();
        }
        
        position = transform.position;
    }

    void Start()
    {
        RecalculateSpawn();
    }


    void Update()
    {
        if (nextSpawn <= 0f)
        {
            pool.Spawn(position);
            RecalculateSpawn();
        }
        else
        {
            nextSpawn -= Time.deltaTime;
        }
    }

    public void RecalculateSpawn()
    {
        nextSpawn = Random.Range(spawningRate.x, spawningRate.y);
        nextX = Random.Range(AxisRange.x, AxisRange.y);
        position = new Vector3(nextX, 0, position.z);
    }
}

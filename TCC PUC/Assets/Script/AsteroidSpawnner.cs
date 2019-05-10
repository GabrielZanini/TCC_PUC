﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnner : MonoBehaviour
{
    public Vector2 spawningRate;
    public Vector2 yAxisRange;

    float nextSpawn = 0f;
    float nextY = 0f;

    AsteroidPool pool;
    Vector3 position;


    void Awake()
    {
        pool = GetComponent<AsteroidPool>();
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
            pool.SpawnAsteroid(position, Quaternion.identity);
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
        nextY = Random.Range(yAxisRange.x, yAxisRange.y);
        position = new Vector3(position.x, nextY, 0);
    }
}

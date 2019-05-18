using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSpawner : MonoBehaviour
{
    [SerializeField] protected ObjectPool pool;

    [SerializeField] protected float minSpawnRate = 0f;
    [SerializeField] protected float maxSpawnRate = 0f;

    protected Vector3 spawnPosition;
    protected float spawnCounter = 0f;


    void Start()
    {
        SpawnerStart();
    }
    
    void Update()
    {
        if (GameManager.Instance.Level.IsPlaying)
        {
            if (spawnCounter <= 0f)
            {
                pool.Spawn(spawnPosition);
                RecalculateSpawn();
            }
            else
            {
                spawnCounter -= Time.deltaTime;
            }
        }

        SpawnerUpdate();
    }

    protected abstract void SpawnerStart();
    protected abstract void SpawnerUpdate();

    protected abstract void RecalculateSpawn();
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : ObjectSpawner
{
    [Header("Spawning Rate")]
    [SerializeField] protected float minSpawnRate = 0f;
    [SerializeField] protected float maxSpawnRate = 0f;

    private void Update()
    {
        if (GameManager.Instance.Level.State == LevelState.Playing)
        {
            if (spawnCounter <= 0f)
            {
                SpawnRandom();
                spawnCounter = Random.Range(minSpawnRate, maxSpawnRate);
            }
            else
            {
                spawnCounter -= Time.deltaTime;
            }
        }
    }
}

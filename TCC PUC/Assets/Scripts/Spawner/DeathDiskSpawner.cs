using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDiskSpawner : ObjectSpawner
{
    public float spawnDelay = 3f;

    protected override void Start()
    {
        base.Start();
        spawnCounter = spawnDelay;
    }

    private void Update()
    {
        if (GameManager.Instance.Level.State == LevelState.Playing)
        {
            if (pool.ActiveCount == 0)
            {
                if (spawnCounter <= 0f)
                {
                    Spawn();
                    spawnCounter = spawnDelay;
                }
                else
                {
                    spawnCounter -= Time.deltaTime;
                }
            }
        }
    }

}

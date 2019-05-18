using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : ObjectSpawner
{
    float nextX = 0f;
    float lastX = 0f;

    float minAxis = 0f;
    float maxAxis = 0f;
    
    

    protected override void SpawnerStart()
    {
        spawnPosition = new Vector3(0, 0, CameraManager.Instance.height / 2f + 1);
        transform.position = spawnPosition;

        maxAxis = CameraManager.Instance.landscapeSize - 3;
        minAxis = -maxAxis;

        RecalculateSpawn();
    }

    protected override void SpawnerUpdate()
    {
        
    }
    
    protected override void RecalculateSpawn()
    {
        spawnCounter = Random.Range(minSpawnRate, maxSpawnRate);
        nextX = Random.Range(minAxis, maxAxis);

        if (nextX - lastX > -1 && nextX - lastX < 1)
        {
            if (nextX - lastX > 0)
            {
                nextX += -1;
            }
            else
            {
                nextX += 1;
            }
        }

        spawnPosition = new Vector3(nextX, 0, spawnPosition.z);
        lastX = nextX;
    }
}

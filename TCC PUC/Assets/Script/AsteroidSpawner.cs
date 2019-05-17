using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] ObjectPool pool;

    public Vector2 spawningRate;

    float nextSpawn = 0f;
    float nextX = 0f;

    float minAxis = 0f;
    float maxAxis = 0f;

    Vector3 position;


    void Awake()
    {
        if (pool == null)
        {
            pool = GetComponent<AsteroidPool>();
        }
        
        
    }

    void Start()
    {
        position = new Vector3(0, 0, CameraManager.Instance.height / 2f + 1);
        transform.position = position;

        maxAxis = CameraManager.Instance.width / 2f - 1;
        minAxis = -maxAxis;

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
        nextX = Random.Range(minAxis, maxAxis);
        position = new Vector3(nextX, 0, position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPool : MonoBehaviour
{
    public static AsteroidPool Instance { get; private set; }

    public List<GameObject> asteroids = new List<GameObject>();



    public Vector2 spawningRate;
    public Vector2 yAxisRange;

    float nextSpawn = 0f;
    float nextY = 0f;
    Vector3 position;

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

    private void Start()
    {
        position = transform.position;
        RecalculateSpawn();
    }

    private void Update()
    {
        if (nextSpawn <= 0f)
        {
            SpawnAsteroid(position, Quaternion.identity);
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

    public GameObject SpawnAsteroid(Vector3 position, Quaternion rotation)
    {
        if (asteroids.Count == 0)
        {
            return null;
        }

        var asteroid = asteroids[0];
        asteroids.RemoveAt(0);

        asteroid.transform.position = position;
        asteroid.transform.rotation = rotation;
        asteroid.GetComponent<TimeBody>().SetActive(true);

        return asteroid;
    }

    public void DespawnAsteroid(GameObject asteroid)
    {
        asteroid.GetComponent<TimeBody>().SetActive(false);
        asteroids.Add(asteroid);
    }
}

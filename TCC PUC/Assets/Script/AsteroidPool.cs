using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPool : MonoBehaviour
{
    public static AsteroidPool Instance { get; private set; }

    public List<GameObject> asteroids = new List<GameObject>();

    

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

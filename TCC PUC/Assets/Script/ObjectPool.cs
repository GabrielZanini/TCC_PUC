using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();


    void Start()
    {
        for (int i=0; i<objects.Count; i++)
        {
            objects[i].GetComponent<TimeBody>().pool = this;
        }
    }

    public GameObject Spawn()
    {
        if (objects.Count == 0)
        {
            return null;
        }

        var gameObject = objects[0];
        objects.RemoveAt(0);

        gameObject.GetComponent<TimeBody>().SetActive(true);

        return gameObject;
    }

    public GameObject Spawn(Vector3 position)
    {
        var gameObject = Spawn();

        if (gameObject != null)
        {
            gameObject.transform.position = position;
        }

        return gameObject;
    }

    public GameObject Spawn(Vector3 position, Quaternion rotation)
    {
        var gameObject = Spawn();

        if (gameObject != null)
        {
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
        }

        return gameObject;
    }


    public void Despawn(GameObject gameObject)
    {
        gameObject.GetComponent<TimeBody>().SetActive(false);
        objects.Add(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] List<TimeBody> activeObjects = new List<TimeBody>();
    [SerializeField] List<TimeBody> inactiveObjects = new List<TimeBody>();

    public int ActiveCount {
        get { return activeObjects.Count; }
    }
    public int InactiveCount {
        get { return inactiveObjects.Count; }
    }

    void Start()
    {
        GetTimebodys();
        DespawnAll();
        SetPool();
    }


    public TimeBody Spawn()
    {
        if (inactiveObjects.Count == 0)
        {
            return null;
        }

        var timebody = inactiveObjects[0];

        inactiveObjects.RemoveAt(0);
        activeObjects.Add(timebody);

        timebody.SetActive(true);

        return timebody;
    }

    public TimeBody Spawn(Vector3 position)
    {
        var timebody = Spawn();

        if (timebody != null)
        {
            timebody.transform.position = position;
        }

        return timebody;
    }

    public TimeBody Spawn(Vector3 position, Quaternion rotation)
    {
        var timebody = Spawn();

        if (timebody != null)
        {
            timebody.transform.position = position;
            timebody.transform.rotation = rotation;
        }

        return timebody;
    }

    public void DespawnAll()
    {
        for (int i=0; i<activeObjects.Count; i++)
        {
            Despawn(activeObjects[i]);
        }
    }

    public void Despawn(TimeBody timebody)
    {
        timebody.SetActive(false);

        activeObjects.Remove(timebody);
        inactiveObjects.Add(timebody);
    }

    private void GetTimebodys()
    {
        var timebodys = GetComponentsInChildren<TimeBody>();

        for (int i=0; i<timebodys.Length; i++)
        {
            inactiveObjects.Add(timebodys[i]);
        }
    }

    private void SetPool()
    {
        for (int i = 0; i < inactiveObjects.Count; i++)
        {
            inactiveObjects[i].pool = this;
        }
    }
}

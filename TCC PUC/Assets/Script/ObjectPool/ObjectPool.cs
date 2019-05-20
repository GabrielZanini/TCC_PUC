using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    [SerializeField] int maxCount = 10;
    public int MaxCount {
        get { return maxCount; }
        private set { maxCount = value; }
    }


    [SerializeField] List<TimeBody> activeObjects = new List<TimeBody>();
    [SerializeField] List<TimeBody> inactiveObjects = new List<TimeBody>();

    public int ActiveCount {
        get { return activeObjects.Count; }
    }
    public int InactiveCount {
        get { return inactiveObjects.Count; }
    }
    public int TotalCount {
        get { return InactiveCount + ActiveCount; }
    }



    void Start()
    {
        GetTimebodys();
        DespawnAll();
        SetPool();

        GameManager.Instance.Level.OnBeforeStart.AddListener(DespawnAll);
    }

    private void OnDestroy()
    {
        GameManager.Instance.Level.OnBeforeStart.AddListener(DespawnAll);
    }



    public TimeBody Spawn()
    {
        if (InactiveCount == 0)
        {
            if (maxCount > TotalCount && prefab != null)
            {
                CreateObject();
            }
            else
            {
                return null;
            }            
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
        
    public TimeBody SpwanRandom()
    {
        if (InactiveCount == 0)
        {
            if (maxCount > TotalCount && prefab != null)
            {
                CreateObject();
            }
            else
            {
                return null;
            }
        }

        int index = Random.Range(0, inactiveObjects.Count);
        var timebody = inactiveObjects[index];

        inactiveObjects.RemoveAt(index);
        activeObjects.Add(timebody);

        timebody.SetActive(true);

        return timebody;
    }



    public void DespawnAll()
    {
        //Debug.Log(this.name + " - DespawnAll");

        while (activeObjects.Count > 0)
        {
            Despawn(activeObjects[0]);
        }
    }

    public void Despawn(TimeBody timebody)
    {
        timebody.SetActive(false);

        activeObjects.Remove(timebody);
        inactiveObjects.Add(timebody);
    }


    public void DestroyObject(TimeBody timebody)
    {
        activeObjects.Remove(timebody);
        inactiveObjects.Remove(timebody);

        Destroy(timebody.gameObject);
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

    private void CreateObject()
    {
        if (prefab != null)
        {
            var timebody = Instantiate(prefab, transform).GetComponent<TimeBody>();
            timebody.pool = this;
            Despawn(timebody);
        }        
    }
}

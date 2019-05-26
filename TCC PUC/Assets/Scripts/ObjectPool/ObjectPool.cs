using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] PoolsManager manager;
    public PoolsManager Manager {
        get { return manager; }
        private set { manager = value; }
    }

    [Header("Prefabs")]
    [SerializeField] List<GameObject> prefabs = new List<GameObject>();

    [Header("Settings")]
    [SerializeField] int minCount = 0;
    public int MinCount {
        get { return minCount; }
        private set { minCount = value; }
    }
    [SerializeField] int maxCount = 10;
    public int MaxCount {
        get { return maxCount; }
        private set { maxCount = value; }
    }

    [Header("Objects")]
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


    private void Reset()
    {
        Manager = GetComponentInParent<PoolsManager>();
    }

    private void OnValidate()
    {
        Manager = GetComponentInParent<PoolsManager>();
    }

    void Start()
    {
        GetTimebodys();
        CreateMinimum();
        DespawnAll();
        SetPool();

        GameManager.Instance.Level.OnMenu.AddListener(DespawnAll);
        GameManager.Instance.Level.OnStart.AddListener(DespawnAll);
    }

    private void OnDestroy()
    {
        GameManager.Instance.Level.OnMenu.RemoveListener(DespawnAll);
        GameManager.Instance.Level.OnStart.RemoveListener(DespawnAll);
    }



    public TimeBody Spawn()
    {
        if (InactiveCount == 0)
        {
            if (maxCount > TotalCount && prefabs.Count > 0)
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

        timebody.OnSpawn.Invoke();

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
            if (maxCount > TotalCount && prefabs.Count > 0)
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


    public void CreateMinimum()
    {
        while (TotalCount < MinCount)
        {
            CreateObject();
        }
    }

    public void DespawnAll()
    {
        //Debug.Log(this.name + " - DespawnAll");

        while (activeObjects.Count > 0)
        {
            Despawn(activeObjects[0]);
        }
    }

    public void CheckLists(TimeBody timebody)
    {
        if (timebody.isActive)
        {
            inactiveObjects.Remove(timebody);

            if (!activeObjects.Contains(timebody))
            {
                activeObjects.Add(timebody);
            }
        }
        else
        {
            activeObjects.Remove(timebody);

            if (!inactiveObjects.Contains(timebody))
            {
                inactiveObjects.Add(timebody);
            }
        }

        timebody.SetActive(timebody.isActive);
    }

    public void Despawn(TimeBody timebody)
    {
        timebody.SetActive(false);
        CheckLists(timebody);
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
        if (prefabs.Count > 0)
        {
            int index = TotalCount % prefabs.Count;

            var timebody = Instantiate(prefabs[index], transform).GetComponent<TimeBody>();
            timebody.pool = this;
            timebody.controller = Manager.GameManager.TimeController;

            Despawn(timebody);
        }        
    }
}

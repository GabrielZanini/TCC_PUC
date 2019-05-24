using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Camera")]
    public CameraManager camerManager;
    public float margin = 1f;

    [Header("Object Pool")]
    [SerializeField] protected ObjectPool pool;

    [Header("Spawn Points")]
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();

    [Header("Number of Objects")]
    public int numberOfObjects = 1;
           
    List<int> spawnIds = new List<int>();
    List<int> usedIds = new List<int>();

    protected float spawnCounter = 0f;

    

    private void Awake()
    {
        AjustSpawnPoints();
        CreateIdList();
        AddListeners();
    }

    protected virtual void Start()
    {
        SetSpawner();
    }

    void OnDestroy()
    {
        RemoveListeners();
    }



    void AddListeners()
    {
        if (camerManager != null)
        {
            camerManager.OnChange.AddListener(SetSpawner);
        }
    }

    void RemoveListeners()
    {
        if (camerManager != null)
        {
            camerManager.OnChange.RemoveListener(SetSpawner);
        }
    }



    void SetSpawner()
    {
        transform.position = new Vector3(0f, 0f, camerManager.verticalSize + 1);
        transform.localScale = new Vector3(camerManager.horizontalSize - 2 * margin, 1f, 1f);
    }


    public void Spawn()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            pool.Spawn(spawnPoints[i].position);
        }
    }
    
    public void SpawnAt(int index)
    {
        if (index < spawnPoints.Count)
        {
            pool.Spawn(spawnPoints[index].position);
        }        
    }

    public void SpawnRandom()
    {
        int index;
        usedIds.Clear();

        for (int i = 0; i < numberOfObjects; i++)
        {
            if (spawnIds.Count == 0)
            {
                CreateIdList();
                usedIds.Clear();
            }
            
            index = Random.Range(0, spawnIds.Count - 1);

            spawnIds.Remove(index);
            usedIds.Add(index);

            pool.Spawn(spawnPoints[index].position);
        }

        CreateIdList();
        usedIds.Clear();
    }

    void CreateIdList()
    {
        spawnIds.Clear();

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            spawnIds.Add(i);
        }
    }

    void AjustSpawnPoints()
    {
        float distance = 2f / (spawnPoints.Count - 1f);

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            spawnPoints[i].localPosition = new Vector3(-1 + (i * distance), 0f, 0f);
        }
    }
}
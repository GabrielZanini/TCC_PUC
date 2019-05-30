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
    [Range(1, 5)] public int minObject = 1;
    [Range(1, 5)] public int maxObject = 3;

    List<int> spawnIds = new List<int>();
    List<int> usedIds = new List<int>();

    protected float spawnCounter = 0f;




    private void OnValidate()
    {
        if (minObject > maxObject)
        {
            maxObject = minObject;
        }

        SetSpawner();
        ResetObjectCount();
        CreateIdList();
    }

    private void Awake()
    {
        SetSpawner();
        ResetObjectCount();
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
        if (camerManager != null)
        {
            transform.position = new Vector3(0f, camerManager.verticalSize + 1, 0f);
            transform.localScale = new Vector3(camerManager.horizontalSize - 2 * margin, 1f, 1f);
        }
    }


    public void Spawn()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            pool.Spawn(spawnPoints[i].position);
        }

        ResetObjectCount();
    }
    
    public void SpawnAt(int index)
    {
        if (index < numberOfObjects)
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
        //float distance = 2f / (spawnPoints.Count - 1f);

        //for (int i = 0; i < spawnPoints.Count; i++)
        //{
        //    spawnPoints[i].localPosition = new Vector3(-1 + (i * distance), 0f, 0f);
        //}

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (i < numberOfObjects)
            {
                spawnPoints[i].gameObject.SetActive(true);
            }
            else
            {
                spawnPoints[i].gameObject.SetActive(false);
            }
        }
        
        if (numberOfObjects > 1)
        {
            float distance = 2f / (numberOfObjects - 1f);

            for (int i = 0; i < numberOfObjects; i++)
            {
                spawnPoints[i].localPosition = new Vector3(-1 + (i * distance), 0f, 0f);
            }
        }
        else
        {
            spawnPoints[0].localPosition = Vector3.zero;
        }
        
    }

    private void ResetObjectCount()
    {
        numberOfObjects = Random.Range(minObject, maxObject + 1);
        AjustSpawnPoints();
    }
}
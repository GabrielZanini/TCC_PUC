using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    [Header("Camera")]
    public CameraManager camerManager;

    [Header("Spawners")]
    public List<ObjectSpawner> spawners = new List<ObjectSpawner>();


    void Reset()
    {
        GetSpawners();
    }

    void Start()
    {
        GetSpawners();
    }

    void OnValidate()
    {
        GetSpawners();
    }

    void GetSpawners()
    {
        var sp = GetComponentsInChildren<ObjectSpawner>();

        spawners.Clear();

        for (int i=0; i<sp.Length; i++)
        {
            spawners.Add(sp[i]);
        }

        SetCamera();
    }

    void SetCamera()
    {
        if (camerManager != null)
        {
            for (int i = 0; i < spawners.Count; i++)
            {
                spawners[i].camerManager = camerManager;
            }
        }
    }
}

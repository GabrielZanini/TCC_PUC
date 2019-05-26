using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotateObject : MonoBehaviour
{
    [Header("Mesh Rotation")]
    public Transform mesh;
    public float minRotation = -5f;
    public float maxRotation = 5;


    Vector3 rotation;

    void Start()
    {
        ResetRotation();
    }

    void OnEnable()
    {
        ResetRotation();
    }

    void Update()
    {
        Rotate();
    }



    void ResetRotation()
    {
        rotation = new Vector3(Random.Range(minRotation, maxRotation), Random.Range(minRotation, maxRotation), Random.Range(minRotation, maxRotation));
    }

    void Rotate()
    {
        mesh.Rotate(rotation);
    }
}

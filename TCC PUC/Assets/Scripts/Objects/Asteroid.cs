using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Transform mesh;

    [Header("Speed")]
    public float minSpeed = 0f;
    public float maxSpeed = 10f;

    [Header("Rotation")]
    public float minRotation = -5f;
    public float maxRotation = 5;

    [Header("Translate Offset")]
    public float minOffset = -0.03f;
    public float maxOffset = 0.03f;

    Vector3 offset = Vector3.zero;
    Vector3 rotation;
    float speed;

    LevelManager level;

    private void Start()
    {
        level = GameManager.Instance.Level;
        rotation = new Vector3(Random.Range(minRotation, maxRotation), Random.Range(minRotation, 5), Random.Range(minRotation, maxRotation));
    }

    void OnEnable()
    {   
        speed = Random.Range(minSpeed, maxSpeed);
        offset.x = Random.Range(minOffset, maxOffset);
    }

    void Update()
    {
        if (GameManager.Instance.Level.State == LevelState.Playing  && !TimeController.Instance.IsRewinding)
        {
            Tranlate();
            Rotate();
        }        
    }
    
    void Tranlate()
    {
        transform.Translate(Vector3.back * speed * level.DifficultyModifire * Time.deltaTime + offset);        
    }

    void Rotate()
    {
        mesh.Rotate(rotation);
    }
}


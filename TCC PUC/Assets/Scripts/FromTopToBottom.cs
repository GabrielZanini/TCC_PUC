using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromTopToBottom : MonoBehaviour
{
    [Header("Speed")]
    public float minSpeed = 0f;
    public float maxSpeed = 10f;
    [Header("Translate Offset")]
    public float minOffset = -0.03f;
    public float maxOffset = 0.03f;

    Vector3 offset = Vector3.zero;
    float speed;
    
    
    protected virtual void Start()
    {

    }

    void OnEnable()
    {   
        speed = Random.Range(minSpeed, maxSpeed);
        offset.x = Random.Range(minOffset, maxOffset);
    }

    protected virtual void Update()
    {
        Tranlate();
    }
    
    void Tranlate()
    {
        transform.Translate(Vector3.back * speed * GameManager.Instance.Level.DifficultyModifire * Time.deltaTime + offset);        
    }
}


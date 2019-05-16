﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Transform mesh;

    StatusBase status;
    Vector3 rotation;
    float speed;
    public float lifeTime = 3f;

    float despawnCounter = 0f;
    TimeBody timebody;

    private void Awake()
    {
        timebody = GetComponent<TimeBody>();
        status = GetComponent<StatusBase>();
    }


    void Start()
    {
        status.OnDeath.AddListener(Death);
    }

    void Destroy()
    {
        status.OnDeath.RemoveListener(Death);
    }



    void OnEnable()
    {
        despawnCounter = lifeTime;
        status.currentHp = status.maxHp;

        rotation = new Vector3(Random.Range(-5,5), Random.Range(-5, 5), Random.Range(-5, 5));
        speed = Random.Range(status.currentSpeed, status.maxSpeed);
    }

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (despawnCounter <= 0f)
        {
            timebody.Despawn();
        }
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    void Rotate()
    {
        mesh.Rotate(rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter - " + gameObject.name);

        var otherStatus = other.gameObject.GetComponent<StatusBase>();

        if (otherStatus != null)
        {
            otherStatus.AddHp(-status.currentHp);
            timebody.Despawn();
        }
    }

    void Death()
    {
        //Boom

        timebody.Despawn();
    }
}


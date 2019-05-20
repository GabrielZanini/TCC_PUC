using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Transform mesh;

    StatusBase status;
    Vector3 offset;
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
        
    void OnEnable()
    {
        despawnCounter = lifeTime;
        status.CurrentHp = status.MaxHp;

        rotation = new Vector3(Random.Range(-5,5), Random.Range(-5, 5), Random.Range(-5, 5));
        speed = Random.Range(status.currentSpeed, status.maxSpeed);
        offset = new Vector3(Random.Range(-0.03f, 0.03f), 0, 0);
    }

    void Update()
    {
        if (!GameManager.Instance.Level.IsPaused)
        {
            Tranlate();
        }        
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(gameObject.name + " - OnTriggerEnter - " + other.gameObject.name);

        var otherStatus = other.gameObject.GetComponent<StatusBase>();

        if (otherStatus != null)
        {
            otherStatus.TakeDamage(status.CurrentHp);
            timebody.Despawn();
        }
    }

    void OnDestroy()
    {
        status.OnDeath.RemoveListener(Death);
    }



    void Tranlate()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime + offset);

        if (despawnCounter <= 0f)
        {
            timebody.Despawn();
        }
    }

    void Rotate()
    {
        mesh.Rotate(rotation);
    }

    void Death()
    {
        //Boom

        timebody.Despawn();
    }
}


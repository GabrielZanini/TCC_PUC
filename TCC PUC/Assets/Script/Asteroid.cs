using System.Collections;
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

    

    private void Awake()
    {
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
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (despawnCounter <= 0f)
        {
            BulletPool.Instance.DespawnBullet(gameObject);
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
            AsteroidPool.Instance.DespawnAsteroid(gameObject);
        }
    }

    void Death()
    {
        //Boom

        AsteroidPool.Instance.DespawnAsteroid(gameObject);
    }
}


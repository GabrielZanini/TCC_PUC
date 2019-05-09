using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;
    public int damage;

    float despawnCounter = 0f;


    private void OnEnable()
    {
        despawnCounter = lifeTime;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (despawnCounter <= 0f)
        {
            BulletPool.Instance.DespawnBullet(gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter - " + gameObject.name + " - Collision: " + other.name);

        var otherStatus = other.gameObject.GetComponent<StatusBase>();


        if (otherStatus != null)
        {
            Debug.Log("Has Status - Collision: " + other.name);
            otherStatus.AddHp(-damage);
        }

        BulletPool.Instance.DespawnBullet(gameObject);
    }
}

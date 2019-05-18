using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] TimeBody timeBody;

    public float speed = 20f;
    public float lifeTime = 2f;
    public int damage;

    float despawnCounter = 0f;

    
    void Awake()
    {
        timeBody.OnActivate.AddListener(ResetCounter);
    }

    void Start()
    {
        ResetCounter();
    }

    void OnDestroy()
    {
        timeBody.OnActivate.RemoveListener(ResetCounter);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);        
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherStatus = other.gameObject.GetComponent<StatusBase>();
        
        if (otherStatus != null)
        {
            otherStatus.TakeDamage(damage);
        }

        timeBody.Despawn();
    }

    void ResetCounter()
    {
        despawnCounter = lifeTime;
    }
}

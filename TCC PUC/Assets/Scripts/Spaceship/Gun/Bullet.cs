using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TimeBody))]
public class Bullet : MonoBehaviour
{
    [Header("Timebody")]
    [SerializeField] TimeBody timeBody;

    [Header("Settings")]
    public float speed = 20f;
    public float lifeTime = 2f;
    public int damage;

    [Header("Render")]
    public SpriteRenderer inRender;
    public SpriteRenderer outRender;

    float despawnCounter = 0f;


    private void Reset()
    {
        timeBody = GetComponent<TimeBody>();
    }

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

            GameManager.Instance.Pools.TinyExplosion.Spawn(transform.position);
        }

        timeBody.Despawn();
    }

    void ResetCounter()
    {
        despawnCounter = lifeTime;
    }
}

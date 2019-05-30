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
    public Transform Graphics; 
    public SpriteRenderer inRender;
    public SpriteRenderer outRender;
    public bool scaleX;
    public bool scaleY;
    public bool scaleZ;


    float despawnCounter = 0f;


    private void Reset()
    {
        timeBody = GetComponent<TimeBody>();
    }

    void Awake()
    {
        timeBody.OnActivate.AddListener(ResetCounter);
        timeBody.OnSpawn.AddListener(ResetCounter);
        timeBody.OnDespawn.AddListener(ResetCounter);
    }

    void Start()
    {
        ResetCounter();
    }

    void OnDestroy()
    {
        timeBody.OnActivate.RemoveListener(ResetCounter);
        timeBody.OnSpawn.RemoveListener(ResetCounter);
        timeBody.OnDespawn.RemoveListener(ResetCounter);
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

            timeBody.Despawn();
        }
        else
        {
            var otherShield = other.gameObject.GetComponent<Shield>();

            if (otherShield != null)
            {
                if (otherShield.absorb)
                {
                    // ABSORB BULLETS
                }

                if (otherShield.reflect)
                {
                    // REFLECT BULLETS
                }
            }
        }

        timeBody.Despawn();
    }

    void ResetCounter()
    {
        despawnCounter = lifeTime;
    }

    public void SetScale(float scale)
    {
        Vector3 newScale = Vector3.one;

        if (scaleX)
        {
            newScale.x = scale;
        }

        if (scaleY)
        {
            newScale.y = scale;
        }

        if (scaleZ)
        {
            newScale.z = scale;
        }

        Graphics.localScale = newScale;
    }
}

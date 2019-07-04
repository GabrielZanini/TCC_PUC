using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TimeBody))]
public abstract class PickUp : MonoBehaviour
{
    public TimeBody timebody;
    protected PowerUpPool pool;

    public UnityEvent OnPicked;

    private void Reset()
    {
        timebody = GetComponent<TimeBody>();

        if (timebody.pool != null && gameObject.name == "PowerUp(Clone)")
        {
            pool = (PowerUpPool)timebody.pool;
        }
    }

    private void OnValidate()
    {
        Reset();
    }

    private void Start()
    {
        Reset();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponentInParent<PlayerManager>();

        if (player != null)
        {
            PlayerPickUp(player);
            OnPicked.Invoke();
            timebody.Despawn();
        }
    }
    
    protected abstract void PlayerPickUp(PlayerManager player);
    
}

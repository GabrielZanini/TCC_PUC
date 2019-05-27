using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TimeBody))]
public abstract class PickUp : MonoBehaviour
{
    public TimeBody timebody;



    private void Reset()
    {
        timebody = GetComponent<TimeBody>();
    }

    private void OnValidate()
    {
        Reset();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponentInParent<PlayerManager>();

        if (player != null)
        {
            PlayerPickUp(player);            
            timebody.Despawn();
        }
    }
    
    protected abstract void PlayerPickUp(PlayerManager player);
    
}

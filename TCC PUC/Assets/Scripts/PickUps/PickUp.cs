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
        Debug.Log("COlLOSION - ");
        var player = other.gameObject.GetComponent<PlayerManager>();

        if (player != null)
        {
            PlayerPickUp(player);

            Debug.Log("Player");
            timebody.Despawn();
        }
    }
    
    protected abstract void PlayerPickUp(PlayerManager player);
    
}

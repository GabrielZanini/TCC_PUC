using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var timebody = other.gameObject.GetComponent<TimeBody>();

        if (timebody != null)
        {
            timebody.Despawn();
        }
    }
}

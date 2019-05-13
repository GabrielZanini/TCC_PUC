using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var timeBody = other.gameObject.GetComponent<TimeBody>();

        if (timeBody != null)
        {
            timeBody.Despawn();
        }
    }
}

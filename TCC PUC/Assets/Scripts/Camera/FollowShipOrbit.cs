using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowShipOrbit : MonoBehaviour
{
    public Transform targetShip;
    public float offset = 10f;

    public float speed = 10f;


    
    void Update()
    {
        Follow();

        transform.LookAt(targetShip);
    }

    void Follow()
    {
        transform.position = Vector3.Lerp(transform.position, targetShip.position + (targetShip.up * offset), speed * Time.deltaTime);
    } 
}

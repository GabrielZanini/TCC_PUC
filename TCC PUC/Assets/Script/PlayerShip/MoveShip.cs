using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShip : MonoBehaviour
{
    public float speed = 1f;
    public float angularSpeed = 1f;
    public bool canGoBackwards = false;

    public Transform orbitPlanet;

    public float v;
    public float h;

    TimeBody timeBody;

    void Start()
    {
        timeBody = GetComponent<TimeBody>();
    }



    void Update()
    {
        if (timeBody.isRewinding)
        {
            return;
        }

        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
         
        Move();
        Rotate();

        Orbit();
    }



    private void Move()
    {
        if (!canGoBackwards && v < 0)
        {
            return;
        }

        transform.Translate(Vector3.forward * v * speed * Time.deltaTime);
    }

    private void Rotate()
    {
        if (canGoBackwards && v < 0)
        {
            h *= -1;
        } 

        transform.Rotate(Vector3.up * h * angularSpeed * Time.deltaTime);
    }

    private void Orbit()
    {
        if (orbitPlanet == null)
            return;

        // Rotate Ship
        Vector3 gravityUp = (transform.position - orbitPlanet.position).normalized;
        Vector3 shipUp = Vector3.up;

        

    }
}

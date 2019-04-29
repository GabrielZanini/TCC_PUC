using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShip : MonoBehaviour
{
    public float speed = 1f;
    public float angularSpeed = 1f;
    public bool canGoBackwards = false;

    public Transform orbitPlanet;

    public bool canRotate;

    float v;
    float h;

    TimeBody timeBody;
    ShipInput input;

    void Start()
    {
        timeBody = GetComponent<TimeBody>();
        input = GetComponent<ShipInput>();

        if (input == null)
        {
            Destroy(this);
        }
    }



    void Update()
    {
        if (TimeController.Instance.isRewinding)
        {
            return;
        }

        v = input.vertical;
        h = input.horizontal;

        MoveHorizontal();
        MoveVertical();

        Orbit();
    }



    private void MoveHorizontal()
    {
        if (!canGoBackwards && v < 0)
        {
            return;
        }

        transform.Translate(Vector3.forward * h * speed * Time.deltaTime);
    }

    private void MoveVertical()
    {
        if (canRotate)
        {
            Rotate();
        }
        else
        {
            transform.Translate(Vector3.up * v * speed * Time.deltaTime);
        }        
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

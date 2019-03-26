using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShip : MonoBehaviour
{
    public float speed = 1f;
    public float angularSpeed = 1f;
    public bool canGoBackwards = false;

    public Transform target;

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
}

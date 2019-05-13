using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShip : MonoBehaviour
{
    public bool canGoBackwards = false;

    public bool canRotate;

    float v;
    float h;

    StatusShip status;
    ShipInput input;
    Animator animator;

    void Start()
    {
        status = GetComponent<StatusShip>();
        input = GetComponent<ShipInput>();
        animator = GetComponent<Animator>();

        if (input == null)
        {
            Destroy(this);
        }
    }



    void Update()
    {
        v = input.vertical;
        h = input.horizontal;

        MoveHorizontal();
        MoveVertical();
    }



    private void MoveHorizontal()
    {
        if (!canGoBackwards && v < 0)
        {
            return;
        }

        transform.Translate(Vector3.forward * h * status.currentSpeed * Time.deltaTime);
    }

    private void MoveVertical()
    {
        if (canRotate)
        {
            Rotate();
        }
        else
        {
            transform.Translate(Vector3.up * v * status.currentSpeed * Time.deltaTime);
        }

        animator.SetFloat("Vertical", v);
    }

    private void Rotate()
    {
        if (canGoBackwards && v < 0)
        {
            h *= -1;
        } 

        transform.Rotate(Vector3.up * h * status.angularSpeed * Time.deltaTime);
    }
    
}

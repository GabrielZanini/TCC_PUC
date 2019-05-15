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

    public bool hasTouchInput = false;
    Touch touch;
    Vector3 touchPosition;
    Vector3 touchOriginalPosition;
    Vector3 shipOriginalPosition;
    Vector3 shipOffsetPosition;

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

        MoveTouch();
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

    private void MoveTouch()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (!hasTouchInput)
            {
                hasTouchInput = true;

                touchOriginalPosition = touchPosition;
                shipOriginalPosition = transform.position;
            }

            shipOffsetPosition = touchPosition - touchOriginalPosition;

            //touchPosition.z = 0;

            transform.position = Vector3.Lerp(transform.position, shipOriginalPosition + shipOffsetPosition, 0.2f);
        }
        else
        {
            hasTouchInput = false;
        }
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

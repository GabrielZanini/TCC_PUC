using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAi : ShipInput
{
    [Header("AI Setting")]
    public AiType type = AiType.Asteroid;
    [Space]
    [Range(-1f, 1f)] public float minHorizontal = 0f;
    [Range(-1f, 1f)] public float maxHorizontal = 0f;
    [Space]
    [Range(-1f, 1f)] public float minVertical = 0f;
    [Range(-1f, 1f)] public float maxVertical = 0f;
    [Space]
    [Range(-1f, 1f)] public float minRotation = 0f;
    [Range(-1f, 1f)] public float maxRotation = 0f;

    [Header("Current")]
    public float horizontal = 0f;
    public float vertical = 0f;
    public float rotation = 0f;

    [Header("Current")]
    public ShootShip shoot;

    private bool goLeft;


    private void OnValidate()
    {
        if (minHorizontal > maxHorizontal)
        {
            maxHorizontal = minHorizontal;
        }

        if (minVertical > maxVertical)
        {
            minVertical = maxVertical;
        }
    }

    private void OnEnable()
    {
        SetMovement();
        SetCombat();
    }

    private void Update()
    {
        if (type == AiType.Boss)
        {
            BossBehavior();
        }
    }

    

    void SetMovement()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        MoveHorizontal();
        MoveVertical();
    }

    private void MoveHorizontal()
    {
        horizontalAxis.SetFixValue(Random.Range(minHorizontal, maxHorizontal));
        horizontal = horizontalAxis.Raw;
    }

    private void MoveVertical()
    {
        verticalAxis.SetFixValue(Random.Range(minVertical, maxVertical));
        vertical = verticalAxis.Raw;
    }

    private void Rotate()
    {
        rotationAxis.SetFixValue(Random.Range(minRotation, maxRotation));
        rotation = rotationAxis.Raw;
    }


    void SetCombat()
    {
        if (autoShoot)
        {
            shootButton.SetFixValue(true, false, false);
        }
    }



    void BossBehavior()
    {      

        if (transform.position.y > 12)
        {
            verticalAxis.SetFixValue(-1);
            autoShoot = false;
        } 
        else
        {
            verticalAxis.SetFixValue(0);
            autoShoot = true;
            shoot.PullTriggers();

            if (goLeft)
            {
                if (transform.position.x <= -5f)
                {
                    goLeft = false;
                }
                else
                {
                    horizontalAxis.SetFixValue(-1);
                }
            }
            else
            {
                if (transform.position.x >= 5f)
                {
                    goLeft = true;
                }
                else
                {
                    horizontalAxis.SetFixValue(1);
                }
            }
        }
    }

}

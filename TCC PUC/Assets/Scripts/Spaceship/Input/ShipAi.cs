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

    private void Start()
    {
        
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
    }

    private void MoveVertical()
    {
        verticalAxis.SetFixValue(Random.Range(minVertical, maxHorizontal));
    }

    private void Rotate()
    {
        rotationAxis.SetFixValue(Random.Range(minRotation, maxRotation));
    }


    void SetCombat()
    {
        if (autoShoot)
        {
            shootButton.SetFixValue(true, false, false);
        }
    }


}

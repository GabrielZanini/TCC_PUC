using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAi : ShipInput
{
    [Header("AI Setting")]
    public AiType type = AiType.Asteroid;
    public CameraManager camera;
    [Range(0f, 1f)] public float randomModifierHorizontal = 0f;
    [Range(0f, 1f)] public float randomModifierVertical = 0f;


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
        if (type == AiType.Asteroid)
        {
            horizontalAxis.SetFixValue(Random.Range(-randomModifierHorizontal, randomModifierHorizontal));
            verticalAxis.SetFixValue(-1);
        }
        else if (type == AiType.DeathCross)
        {
            rotationAxis.SetFixValue(1);
            verticalAxis.SetFixValue(-1);
        }
        else if (type == AiType.SpaceCanon)
        {
            verticalAxis.SetFixValue(-1);
        }
    }

    void SetCombat()
    {
        if (autoShoot)
        {
            shootButton.SetFixValue(true, false, false);
        }
    }
}

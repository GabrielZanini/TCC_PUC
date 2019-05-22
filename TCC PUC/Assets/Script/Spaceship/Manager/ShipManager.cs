using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MoveShip))]
[RequireComponent(typeof(ShipInput))]
[RequireComponent(typeof(ShootShip))]
public class ShipManager : ObjectManager
{
    public MoveShip movement;
    public ShipInput input;
    public ShootShip shoot;

    public Enums.ShipType type = Enums.ShipType.Enemy;

    protected override void Reset()
    {
        base.Reset();
        input = GetComponent<ShipInput>();
        movement = GetComponent<MoveShip>();
        shoot = GetComponent<ShootShip>();
    }

    protected override void Start()
    {
        base.Start();

        Setlayer();
        SetMovement();
        //SetCombat();
    }


    private void Update()
    {
        if (GameManager.Instance.Level.HasStarted)
        {
            Combat();
            Movement();
        }        
    }
    
    
    
    void Combat()
    {
        if (input.shootButton.Down)
        {
            //Debug.Log("Pull Triger");
            shoot.PullTriggers();
        }
        else if (input.shootButton.Up)
        {
            //Debug.Log("Dont Pull Triger");
            shoot.ReleaseTriggers();
        }
        else
        {
            //Debug.Log("NO Triger");
        }
    }

    void Movement()
    {
        if (input.autoMovement)
        {
            movement.MoveToPosition(input.newPosition);
        }
        else
        {
            movement.MoveHorizontal(input.horizontalAxis.Raw);
            movement.MoveVertical(input.verticalAxis.Raw);
        }

        movement.Rotate(input.rotationAxis.Raw);
    }



    void Setlayer()
    {
        string shipLayer;
        string bulletLayer;

        if (type == Enums.ShipType.Player)
        {
            shipLayer = GameManager.Instance.playerLayer;
            bulletLayer = GameManager.Instance.playerBulletLayer;
        }
        else
        {
            shipLayer = GameManager.Instance.enemyLayer;
            bulletLayer = GameManager.Instance.enemyBulletLayer;
        }

        gameObject.layer = LayerMask.NameToLayer(shipLayer);
        shoot.SetBulletsLayer(bulletLayer);
    }

    void SetMovement()
    {
        movement.speed = status.speed;
        movement.angularSpeed = status.angularSpeed;
        movement.smoothness = status.smoothness;
    }

    void SetCombat()
    {
        if (input.autoShoot)
        {
            shoot.PullTriggers();
        }
    }

}

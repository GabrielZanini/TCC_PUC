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

    public ShipType type = ShipType.Enemy;
    public bool playAfterStop = false;




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


    protected override void Update()
    {
        if ((GameManager.Instance.Level.State == LevelState.Playing) 
            || (GameManager.Instance.Level.State == LevelState.Stoped && playAfterStop))
        {
            Combat();
            Movement();
        }       
    }
    
    
    
    void Combat()
    {
        if (input.shootButton.Down)
        {
            shoot.PullTriggers();
        }
        else if (input.shootButton.Up)
        {
            shoot.ReleaseTriggers();
        }
    }

    void Movement()
    {
        if (input.instantMovement)
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

        if (type == ShipType.Player)
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

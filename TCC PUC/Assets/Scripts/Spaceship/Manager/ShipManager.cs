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

    [HideInInspector] public ShipType type = ShipType.Enemy;

    protected ShipStatus shipStatus;



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
        SetHealth();
        SetMovement();
        SetCombat();
    }


    protected override void Update()
    {
        if ((GameManager.Instance.Level.State == LevelState.Playing) 
            || (GameManager.Instance.Level.State == LevelState.Stoped && shipStatus.playAfterStop))
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

    protected virtual void SetHealth()
    {
        health.MaxHp = shipStatus.hp;
        health.scaleWithDificulty = shipStatus.scaleHealth;
    }

    protected virtual void SetMovement()
    {
        movement.speed = shipStatus.speed;
        movement.angularSpeed = shipStatus.angularSpeed;
        movement.smoothness = shipStatus.smoothness;
        movement.scaleWithDificulty = shipStatus.scaleMovement;
    }

    protected virtual void SetCombat()
    {
        shoot.SetBullets(shipStatus.bullets);
        shoot.SetDamage(shipStatus.damage);
        shoot.SetRate(shipStatus.shootingRate);
        shoot.scaleWithDificulty = shipStatus.scaleCombat;
        shoot.SetScaleWithDifficulty();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TimeBody))]
[RequireComponent(typeof(StatusBase))]
[RequireComponent(typeof(MoveShip))]
[RequireComponent(typeof(ShipInput))]
[RequireComponent(typeof(ShootShip))]
public class ShipManager : MonoBehaviour
{
    public TimeBody timebody;
    public StatusBase status;
    public MoveShip movement;
    public ShipInput input;
    public ShootShip shoot;

    public bool isPlayer = false;

    private void Reset()
    {
        timebody = GetComponent<TimeBody>();
        input = GetComponent<ShipInput>();
        status = GetComponent<StatusBase>();
        movement = GetComponent<MoveShip>();
        shoot = GetComponent<ShootShip>();
    }

    private void Start()
    {
        AddListeners();

        if (input.autoshoot)
        {
            shoot.PullTriggers();
        }
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void Update()
    {
        if (!input.autoshoot)
        {
            if (input.shoot.Down)
            {
                shoot.PullTriggers();
            } 
            else if (input.shoot.Up)
            {
                shoot.ReleaseTriggers();
            }
        }
    }



    // Listeners

    void AddListeners()
    {
        status.OnDeath.AddListener(Die);
        status.OnLoseHp.AddListener(Vibrate);

        GameManager.Instance.Level.OnRestart.AddListener(Revive);
    }

    void RemoveListeners()
    {
        status.OnDeath.RemoveListener(Die);
        status.OnLoseHp.RemoveListener(Vibrate);

        GameManager.Instance.Level.OnRestart.RemoveListener(Revive);
    }



    void Die()
    {
        timebody.SetActive(false);
        GameManager.Instance.Level.Stop();
    }

    void Vibrate()
    {
        if (GameManager.Instance.UseVibration)
        {
            Handheld.Vibrate();
        }
    }

    void Revive()
    {
        timebody.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public TimeBody timebody;
    public StatusShip status;
    public MoveShip movement;
    public ShipInput input;
    public ShootShip shoot;

    public bool isPlayer = false;

    private void Start()
    {
        status.OnDeath.AddListener(Die);
        status.OnLoseHp.AddListener(Vibrate);
        GameManager.Instance.Level.OnRestart.AddListener(Revive);
    }

    private void OnDestroy()
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

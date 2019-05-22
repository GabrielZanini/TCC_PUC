using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : ShipManager
{
    [Header("Player Settings")]
    public float spawnMargingBottom = 10f;
    public float marging = 1f;
    public float margingBottom = 5f;


    protected override void Reset()
    {
        base.Reset();
        type = Enums.ShipType.Player;
    }

    protected override void Start()
    {        
        base.Start();
        SetMovementLimits();
        SetMovementStart();
    }




    protected override void AddListeners()
    {
        base.AddListeners();
        status.OnChangeHp.AddListener(Vibrate);
        GameManager.Instance.Level.OnRestart.AddListener(Revive);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        status.OnChangeHp.RemoveListener(Vibrate);
        GameManager.Instance.Level.OnRestart.RemoveListener(Revive);
    }





    void SetMovementStart()
    {
        movement.SetStartPosition(new Vector3(0f, 0f, 0f - CameraManager.Instance.verticalSize + spawnMargingBottom));
    }

    void SetMovementLimits()
    {
        Vector3 min = new Vector3(0 - CameraManager.Instance.horizontalSize + marging, 0, 0 - CameraManager.Instance.verticalSize + marging + margingBottom);
        Vector3 max = new Vector3(CameraManager.Instance.horizontalSize - marging, 0, CameraManager.Instance.verticalSize - marging);

        movement.SetLimits(min, max);
    }



    protected override void Death()
    {
        base.Death();
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
        status.CurrentHp = status.MaxHp;
        timebody.SetActive(true);
    }
}

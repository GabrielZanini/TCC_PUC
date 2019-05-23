using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : ShipManager
{
    [Header("Player Settings")]
    public float spawnMargingBottom = 10f;
    public float marging = 1f;
    public float margingBottom = 5f;

    [Header("Camera")]
    public CameraManager camerManager;


    protected override void Reset()
    {
        base.Reset();
        type = ShipType.Player;
        playAfterStop = false;
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
        status.OnLoseHp.AddListener(Vibrate);
        GameManager.Instance.Level.OnStop.AddListener(shoot.ReleaseTriggers);
        GameManager.Instance.Level.OnRestart.AddListener(Revive);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        status.OnLoseHp.RemoveListener(Vibrate);
        GameManager.Instance.Level.OnStop.RemoveListener(shoot.ReleaseTriggers);
        GameManager.Instance.Level.OnRestart.RemoveListener(Revive);
    }





    void SetMovementStart()
    {
        movement.SetStartPosition(new Vector3(0f, 0f, 0f - camerManager.verticalSize + spawnMargingBottom));
    }

    void SetMovementLimits()
    {
        Vector3 min = new Vector3(0 - camerManager.horizontalSize + marging, 0, 0 - camerManager.verticalSize + marging + margingBottom);
        Vector3 max = new Vector3(camerManager.horizontalSize - marging, 0, camerManager.verticalSize - marging);

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

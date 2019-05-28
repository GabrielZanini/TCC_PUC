using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShieldShip))]
public class PlayerManager : ShipManager
{
    public ShieldShip shield;

    [Header("Camera")]
    public CameraManager camerManager;

    [Header("Player Settings")]
    public Margin margin = new Margin();
    public ShipStyle style;
    public MeshRenderer render;
    public PlayerStatus defaultStaus;
    public PlayerStatus extraStatus;

    [Header("Coins")]
    public int coins = 0;



    protected override void Reset()
    {
        base.Reset();
        shield = GetComponent<ShieldShip>();
        type = ShipType.Player;
        playAfterStop = false;
    }

    private void OnValidate()
    {
        SetStyle();
    }

    protected override void Start()
    {
        base.Start();
        SetMovementPlayer();
    }



    protected override void AddListeners()
    {
        base.AddListeners();
        status.OnLoseHp.AddListener(Vibrate);
        GameManager.Instance.Level.OnStop.AddListener(shoot.ReleaseTriggers);
        GameManager.Instance.Level.OnStop.AddListener(shield.Deactivate);
        GameManager.Instance.Level.OnStart.AddListener(Revive);
        GameManager.Instance.Level.OnMenu.AddListener(Revive);
        camerManager.OnChange.AddListener(SetMovementPlayer);
        //shield.OnActivate.AddListener(timebody.DisableCollider);
        //shield.OnDeactivate.AddListener(timebody.EnableCollider);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        status.OnLoseHp.RemoveListener(Vibrate);
        GameManager.Instance.Level.OnStop.RemoveListener(shoot.ReleaseTriggers);
        GameManager.Instance.Level.OnStop.RemoveListener(shield.Deactivate);
        GameManager.Instance.Level.OnStart.RemoveListener(Revive);
        GameManager.Instance.Level.OnMenu.RemoveListener(Revive);
        camerManager.OnChange.RemoveListener(SetMovementPlayer);
        //shield.OnActivate.RemoveListener(timebody.DisableCollider);
        //shield.OnDeactivate.RemoveListener(timebody.EnableCollider);
    }



    void SetMovementPlayer()
    {
        SetMovementStart();
        SetMovementLimits();
    }

    void SetMovementStart()
    {
        movement.SetStartPosition(Vector3.zero);
    }

    void SetMovementLimits()
    {
        Vector3 min = new Vector3(-camerManager.horizontalSize, -camerManager.verticalSize, 0);
        Vector3 max = new Vector3(camerManager.horizontalSize, camerManager.verticalSize, 0);
        
        movement.SetLimits(min, max, margin);
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
        SetStatus();
    }

    void SetStatus()
    {
        shoot.SetBullets(defaultStaus.bullets + extraStatus.bullets);
    }

    public void SetStyle(ShipStyle newStyle = null)
    {
        if (newStyle != null)
        {
            style = newStyle;
        }

        if (style != null)
        {
            render.material = style.shipMaterial;
            shoot.SetBulletColor(style.inBulletColor, style.outBulletColor);
            shield.SetMaterial(style.shieldMaterial);
        }        
    }


    // Coins

    public void AddCoins(int coins)
    {
        this.coins += coins; 
    }


}

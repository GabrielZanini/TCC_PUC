using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShieldShip))]
public class PlayerManager : ShipManager
{
    public ShieldShip shield;

    [Header("GameManager")]
    [SerializeField] GameManager gameManager;
    public GameManager GameManager {
        get { return gameManager; }
        private set { gameManager = value; }
    }

    [Header("Camera")]
    public CameraManager camera;

    [Header("Player Settings")]
    public Margin margin = new Margin();
    public ShipStyle style;
    public MeshRenderer render;
    public float timeBeforeDeath = 1f;
    public Material dyingMaterial;
    public PlayerStatus defaultStaus;
    public PlayerStatus extraStatus;

    [Header("Coins")]
    public int coins = 0;

    private bool ishandlingDeath = false;

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
        GameManager.Level.OnStop.AddListener(shoot.ReleaseTriggers);
        GameManager.Level.OnStop.AddListener(shield.Deactivate);
        GameManager.Level.OnStart.AddListener(Revive);
        GameManager.Level.OnMenu.AddListener(Revive);
        GameManager.Level.OnMenu.AddListener(shield.Deactivate);
        camera.OnChange.AddListener(SetMovementPlayer);
        //shield.OnActivate.AddListener(timebody.DisableCollider);
        //shield.OnDeactivate.AddListener(timebody.EnableCollider);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        status.OnLoseHp.RemoveListener(Vibrate);
        GameManager.Level.OnStop.RemoveListener(shoot.ReleaseTriggers);
        GameManager.Level.OnStop.RemoveListener(shield.Deactivate);
        GameManager.Level.OnStart.RemoveListener(Revive);
        GameManager.Level.OnMenu.RemoveListener(Revive);
        GameManager.Level.OnMenu.RemoveListener(shield.Deactivate);
        camera.OnChange.RemoveListener(SetMovementPlayer);
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
        Vector3 min = new Vector3(-camera.horizontalSize, -camera.verticalSize, 0);
        Vector3 max = new Vector3(camera.horizontalSize, camera.verticalSize, 0);
        
        movement.SetLimits(min, max, margin);
    }



    protected override void Death()
    {
        if (!ishandlingDeath)
            StartCoroutine(TimeBeforeDeath());       
    }

    IEnumerator TimeBeforeDeath()
    {
        ishandlingDeath = true;

        Material[] normalMaterials = render.materials;
        Material[] dyingmaterials = new Material[normalMaterials.Length + 1];

        for (int i=0; i< normalMaterials.Length; i++)
        {
            dyingmaterials[i] = normalMaterials[i];
        }

        dyingmaterials[normalMaterials.Length] = dyingMaterial;
        input.enabled = false;
        timebody.DisableCollider();

        float timer = timeBeforeDeath;

        while (timer > 0)
        {
            if (!timebody.Controller.IsRewinding || GameManager.Instance.Level.State != LevelState.Playing)
            {
                timer -= Time.deltaTime;

                if (!status.IsDead)
                {
                    break;
                }
            }

            if (status.IsDead)
            {
                render.materials = dyingmaterials;
            }
            else
            {
                render.materials = normalMaterials;
            }
            
            yield return null;
        }

        render.materials = normalMaterials;
        input.enabled = true;
        timebody.EnableCollider();

        if (status.IsDead)
        {
            base.Death();
            render.materials = normalMaterials;
            GameManager.Instance.Level.Stop();
        }

        ishandlingDeath = false;
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
        shoot.SetDamage(defaultStaus.damage + extraStatus.damage);
        shoot.SetRate(defaultStaus.shootingRate + extraStatus.shootingRate);
        shield.duration = defaultStaus.shildTime + extraStatus.shildTime;
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

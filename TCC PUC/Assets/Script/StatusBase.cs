using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatusBase : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHp = 10;
    public int MaxHp {
        get { return maxHp; }
        set { maxHp = value; }
    }
    [HideInInspector] private int currentHp = 0;
    public int CurrentHp {
        get { return currentHp; }
        set {
            currentHp = value;
            OnChangeHp.Invoke();
        }
    }

    [Header("Speed")]
    public float maxSpeed = 1f;
    [HideInInspector] public float currentSpeed = 1f;

    [HideInInspector]public UnityEvent OnGainHp;
    [HideInInspector] public UnityEvent OnLoseHp;
    [HideInInspector] public UnityEvent OnChangeHp;
    [HideInInspector] public UnityEvent OnDeath;

    [HideInInspector] public UnityEvent OnGainSpeed;
    [HideInInspector] public UnityEvent OnLoseSpeed;



    private void Awake()
    {
        InicializeHealth();
    }

    private void Start()
    {
        GameManager.Instance.Level.OnRestart.AddListener(InicializeHealth);
    }

    private void OnDestroy()
    {
        GameManager.Instance.Level.OnRestart.RemoveListener(InicializeHealth);
    }


    
    private void InicializeHealth()
    {
        CurrentHp = MaxHp;
    }


    public void Heal(int health)
    {
        if (health <= 0) return;
                
        if (CurrentHp + health > maxHp)
        {
            CurrentHp = maxHp;
            OnDeath.Invoke();
        }
        else
        {
            CurrentHp += health;
        }

        OnGainHp.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;

        if (CurrentHp - damage <= 0)
        {
            CurrentHp = 0;
            OnDeath.Invoke();
        }
        else
        {
            CurrentHp -= damage;
        }

        OnLoseHp.Invoke();
    }




    public void AddSpeed(float moreSpeed)
    {
        if (moreSpeed == 0) return;

        currentSpeed += moreSpeed;

        if (moreSpeed > 0)
        {
            OnLoseSpeed.Invoke();
        }
        else
        {
            OnGainSpeed.Invoke();
        }

        if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
    }


    
}

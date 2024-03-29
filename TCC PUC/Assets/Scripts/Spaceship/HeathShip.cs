﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeathShip : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHp = 10;
    public int MaxHp {
        get { return maxHp; }
        set { maxHp = value; }
    }

    [SerializeField]
    private int currentHp = 0;
    public int CurrentHp {
        get { return currentHp; }
        set {
            currentHp = value;
            OnChangeHp.Invoke();
        }
    }

    public bool IsDead {
        get { return CurrentHp <= 0; }
    }
    

    [Header("FX")]
    public EffectType deathEffect = EffectType.SmallExplosion;

    [Header("Difficulty")]
    public bool scaleWithDificulty = true;

    [HideInInspector] public UnityEvent OnGainHp;
    [HideInInspector] public UnityEvent OnLoseHp;
    [HideInInspector] public UnityEvent OnChangeHp;
    [HideInInspector] public UnityEvent OnDeath;

    [HideInInspector] public UnityEvent OnGainSpeed;
    [HideInInspector] public UnityEvent OnLoseSpeed;



    private void Awake()
    {
        InicializeHealth();
    }


    private void OnEnable()
    {
        InicializeHealth();
    }


    public void InicializeHealth()
    {
        if (scaleWithDificulty && GameManager.Instance != null)
        {
            CurrentHp = Mathf.RoundToInt(MaxHp * GameManager.Instance.Level.DifficultyModifire);
        }
        else
        {
            CurrentHp = MaxHp;
        }
    }


    // Health

    public void Heal(int health)
    {
        if (health <= 0) return;

        CurrentHp += health;

        if (CurrentHp > maxHp)
        {
            CurrentHp = maxHp;
        }

        OnGainHp.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;

        if (CurrentHp - damage <= 0)
        {
            CurrentHp = 0;
            Death();
        }
        else
        {
            CurrentHp -= damage;
        }

        OnLoseHp.Invoke();
    }
    
    void Death()
    {
        OnDeath.Invoke();
    }
        
}

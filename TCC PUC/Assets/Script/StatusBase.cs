using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatusBase : MonoBehaviour
{
    public int maxHp = 10;
    public int currentHp = 0;
    public float maxSpeed = 1f;
    public float currentSpeed = 1f;

    [HideInInspector]public UnityEvent OnGainHp;
    [HideInInspector] public UnityEvent OnLoseHp;
    [HideInInspector] public UnityEvent OnDeath;

    [HideInInspector] public UnityEvent OnGainSpeed;
    [HideInInspector] public UnityEvent OnLoseSpeed;


    private void Awake()
    {
        currentHp = maxHp;
    }

    public void AddHp(int moreHp)
    {
        //Debug.Log("AddHp - " + moreHp + " - " + gameObject.name);

        if (moreHp == 0) return;

        currentHp += moreHp;

        if (moreHp > 0)
        {
            OnLoseHp.Invoke();
        }
        else
        {
            OnGainHp.Invoke();
        }

        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }

        if (currentHp <= 0)
        {
            OnDeath.Invoke();
        }
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

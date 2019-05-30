using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShieldShip : MonoBehaviour
{
    [Header("GameObject")]
    public Shield shield;

    [Header("Settings")]
    public float duration = 5f;
    public bool isActive = false;
    public float timer = 0f;


    [HideInInspector] public UnityEvent OnActivate;
    [HideInInspector] public UnityEvent OnDeactivate;



    private void Reset()
    {
        shield = GetComponentInChildren<Shield>(true);
    }

    private void OnValidate()
    {
        Reset();
    }

    void Update()
    { 
        if (isActive) 
        {
            if (timer <= 0f)
            {
                Deactivate();
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    public void Activate()
    {
        isActive = true;
        timer = duration;
        shield.gameObject.SetActive(true);
        OnActivate.Invoke();
    }

    public void Deactivate()
    {
        isActive = false;
        shield.gameObject.SetActive(false);
        OnDeactivate.Invoke();
    }

    public void SetMaterial(Material material)
    {
        if (shield != null)
        {
            shield.SetMaterial(material);
        }
    }
}

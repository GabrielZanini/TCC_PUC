using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShieldShip : MonoBehaviour
{
    [Header("GameObject")]
    public GameObject shieldObject;

    [Header("Settings")]
    public float duration = 5f;
    public bool isActive = false;
    public float shieldTimer = 0f;


    [HideInInspector] public UnityEvent OnActivate;
    [HideInInspector] public UnityEvent OnDeactivate;

    private MeshRenderer render;
    


    private void OnValidate()
    {
        if (shieldObject != null)
        {
            render = shieldObject.GetComponent<MeshRenderer>();
        }
    }

    void Update()
    { 
        if (isActive) 
        {
            if (shieldTimer <= 0f)
            {
                Deactivate();
            }
            else
            {
                shieldTimer -= Time.deltaTime;
            }
        }
    }

    public void Activate()
    {
        isActive = true;
        shieldTimer = duration;
        shieldObject.SetActive(true);
        OnActivate.Invoke();
    }

    public void Deactivate()
    {
        isActive = false;
        shieldObject.SetActive(false);
        OnDeactivate.Invoke();
    }

    public void SetMaterial(Material material)
    {
        render.material = material;
    }
}

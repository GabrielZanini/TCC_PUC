using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    public MeshRenderer render;
    
    public bool reflect = false;
    public bool absorb = false;

    private ShieldShip shieldShip;


    
    private void Reset()
    {
        render = GetComponent<MeshRenderer>();
        shieldShip = GetComponentInParent<ShieldShip>();

        if (shieldShip != null)
        {
            shieldShip.shield = this;
        }        
    }

    private void OnValidate()
    {
        Reset();
    }
    


    public void SetMaterial(Material material)
    {
        if (render != null)
        {
            render.material = material;
        }        
    }
}

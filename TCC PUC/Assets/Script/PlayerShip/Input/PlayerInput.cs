using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : ShipInput
{
    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        
    }
}





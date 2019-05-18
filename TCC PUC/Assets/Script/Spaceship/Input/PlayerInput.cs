using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : ShipInput
{
    public bool HasTouch {
        get { return Input.GetKey(KeyCode.Mouse0) || Input.touchCount > 0; }
    }

    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        
    }
}





using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : ShipInput
{
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
    }
}

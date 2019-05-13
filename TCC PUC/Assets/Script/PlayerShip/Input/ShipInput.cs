using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInput : MonoBehaviour
{

    public float vertical = 0;
    public float horizontal = 0;

    public bool shoot = false;

}


public class InputButton
{
    private string _name;

    public bool Up {
        get {
            return Input.GetButtonUp(_name);
        }
    }
    public bool Down {
        get {
            return Input.GetButtonDown(_name);
        }
    }
    public bool Hold {
        get {
            return Input.GetButton(_name);
        }
    }


    public InputButton(string name)
    {
        _name = name;
    }

}

public class InputAxis
{
    private string _name;

    public float Smooth {
        get {
            return Input.GetAxis(_name);
        }
    }
    public float Raw {
        get {
            return Input.GetAxisRaw(_name);
        }
    }

    public InputAxis(string name)
    {
        _name = name;
    }
}
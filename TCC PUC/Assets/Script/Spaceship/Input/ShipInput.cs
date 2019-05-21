using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInput : MonoBehaviour
{    
    public Vector3 newPosition;
    public bool AutoMovemente = true;

    
    public float vertical = 0;
    public float horizontal = 0;

    public InputButtonKey shoot = new InputButtonKey();
    public bool autoshoot = true;



    public Touch touch;



    private void Awake()
    {
        shoot.AddButton("Jump");
    }

}


public class InputButtonKey
{
    private List<string> buttons = new List<string>();
    private List<KeyCode> keys = new List<KeyCode>();
    
    public bool Down {
        get {
            return IsDown();
        }
    }
    public bool Hold {
        get {
            return IsHold();
        }
    }
    public bool Up {
        get {
            return IsUp();
        }
    }

    public void AddButton(string name)
    {
        buttons.Add(name);
    }

    public void RemoveButton(string name)
    {
        buttons.Remove(name);
    }

    public void AddKey(KeyCode key)
    {
        keys.Add(key);
    }

    public void RemovedKey(KeyCode key)
    {
        keys.Remove(key);
    }


    bool IsDown()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (Input.GetButtonDown(buttons[i])) return true;
        }

        for (int i = 0; i < keys.Count; i++)
        {
            if (Input.GetKeyDown(keys[i])) return true;
        }

        return false;
    }

    bool IsHold()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (Input.GetButton(buttons[i])) return true;
        }

        for (int i = 0; i < keys.Count; i++)
        {
            if (Input.GetKey(keys[i])) return true;
        }

        return false;
    }

    bool IsUp()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (Input.GetButtonUp(buttons[i])) return true;
        }

        for (int i = 0; i < keys.Count; i++)
        {
            if (Input.GetKeyUp(keys[i])) return true;
        }

        return false;
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
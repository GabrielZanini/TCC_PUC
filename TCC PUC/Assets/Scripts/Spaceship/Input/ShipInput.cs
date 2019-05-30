using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInput : MonoBehaviour
{
    [Header("Settings")]
    public bool instantMovement = true;
    public bool autoShoot = true;

    // Movement
    [HideInInspector] public Vector3 newPosition;
    [HideInInspector] public Touch touch;

    // Axes
    public InputAxis verticalAxis = new InputAxis();
    public InputAxis horizontalAxis = new InputAxis();
    public InputAxis rotationAxis = new InputAxis();

    // Buttons
    public InputButtonKey shootButton = new InputButtonKey();
}


public class InputButtonKey
{
    private List<string> buttons = new List<string>();
    private List<KeyCode> keys = new List<KeyCode>();
    private List<MobileButton> mobileButtons = new List<MobileButton>();


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


    private bool fixValueDown;
    private bool fixValueHold;
    private bool fixValueUp;
    private bool useFixValue;


    public InputButtonKey()
    {
    }

    public InputButtonKey(KeyCode key)
    {
        AddKey(key);
    }

    public InputButtonKey(string button)
    {
        AddButton(button);
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

    public void AddMobileButton(MobileButton mobileButton)
    {
        mobileButtons.Add(mobileButton);
    }

    public void RemoveMobileButton(MobileButton mobileButton)
    {
        mobileButtons.Remove(mobileButton);
    }

    public void Clear()
    {
        buttons.Clear();
        keys.Clear();
        mobileButtons.Clear();
    }

    public void SetFixValue(bool down, bool hold, bool up)
    {
        useFixValue = true;
        fixValueDown = down;
        fixValueHold = hold;
        fixValueUp = up;
    }

    public void ClearFixValue()
    {
        useFixValue = false;
    }


    bool IsDown()
    {
        if (useFixValue)
        {
            return fixValueDown;
        }

        for (int i = 0; i < buttons.Count; i++)
        {
            if (Input.GetButtonDown(buttons[i])) return true;
        }

        for (int i = 0; i < keys.Count; i++)
        {
            if (Input.GetKeyDown(keys[i])) return true;
        }

        for (int i = 0; i < mobileButtons.Count; i++)
        {
            if (mobileButtons[i].Down) return true;
        }

        return false;
    }

    bool IsHold()
    {
        if (useFixValue)
        {
            return fixValueHold;
        }

        for (int i = 0; i < buttons.Count; i++)
        {
            if (Input.GetButton(buttons[i])) return true;
        }

        for (int i = 0; i < keys.Count; i++)
        {
            if (Input.GetKey(keys[i])) return true;
        }
        
        for (int i = 0; i < mobileButtons.Count; i++)
        {
            if (mobileButtons[i].Hold) return true;
        }

        return false;
    }

    bool IsUp()
    {
        if (useFixValue)
        {
            return fixValueUp;
        }

        for (int i = 0; i < buttons.Count; i++)
        {
            if (Input.GetButtonUp(buttons[i])) return true;
        }

        for (int i = 0; i < keys.Count; i++)
        {
            if (Input.GetKeyUp(keys[i])) return true;
        }

        for (int i = 0; i < mobileButtons.Count; i++)
        {
            if (mobileButtons[i].Up) return true;
        }

        return false;
    }
}

public class InputAxis
{
    private List<string> axes = new List<string>();
    private float axis;

    private float fixValue;
    private bool useFixValue;

    public float Smooth {
        get {
            return GetSmooth();
        }
    }
    public float Raw {
        get {
            return GetRaw();
        }
    }


    public InputAxis()
    {
    }

    public InputAxis(string name)
    {
        AddAxis(name);
    }
    

    public void AddAxis(string name)
    {
        axes.Add(name);
    }

    public void RemoveAxis(string name)
    {
        axes.Remove(name);
    }
    
    public void SetFixValue(float value)
    {
        useFixValue = true;
        fixValue = value;
    }

    public void ClearFixValue()
    {
        useFixValue = false;
    }


    float GetSmooth()
    {
        if (useFixValue)
        {
            return fixValue;
        }

        for (int i = 0; i < axes.Count; i++)
        {
            axis = Input.GetAxis(axes[i]);
            if (axis != 0) return axis;
        }

        return 0f;
    }

    float GetRaw()
    {
        if (useFixValue)
        {
            return fixValue;
        }

        for (int i = 0; i < axes.Count; i++)
        {
            axis = Input.GetAxisRaw(axes[i]);
            if (axis != 0) return axis;
        }

        return 0f;
    }
}
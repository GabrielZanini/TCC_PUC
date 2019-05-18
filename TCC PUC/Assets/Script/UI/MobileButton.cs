using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MobileButton : MonoBehaviour
{
    [SerializeField] bool up = false;
    public bool Up {
        get { return up; }
        private set { up = value; }
    }
    [SerializeField] bool down = false;
    public bool Down {
        get { return down; }
        private set { down = value; }
    }
    [SerializeField] bool hold = false;
    public bool Hold {
        get { return hold; }
        private set { hold = value; }
    }


    [HideInInspector] public UnityEvent OnPress;
    [HideInInspector] public UnityEvent OnRelease;



    public void Press()
    {
        //Debug.Log("MobileButton - Press");
        StartCoroutine(ButtonDown());
        hold = true;
        OnPress.Invoke();
    }

    public void Release()
    {
        //Debug.Log("MobileButton - Release");
        StartCoroutine(ButtonUp());
        hold = false;
        OnRelease.Invoke();
    }


    IEnumerator ButtonUp()
    {
        up = true;
        yield return null;
        up = false;
    }

    IEnumerator ButtonDown()
    {
        down = true;
        yield return null;
        down = false;
    }
}

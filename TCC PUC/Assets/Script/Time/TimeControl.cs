using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance { get; private set; }

    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

        }
        
        if(Input.GetButtonUp("Fire1"))
        {

        }
    }
}

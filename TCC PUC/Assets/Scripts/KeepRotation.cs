using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepRotation : MonoBehaviour
{
    public bool defaultZero = false;

    Quaternion rotation;
    

    void Start()
    {
        if (defaultZero)
        {
            rotation = Quaternion.identity;
        }
        else
        {
            rotation = transform.rotation;
        }
    }

    void Update()
    {
        transform.rotation = rotation;
    }
}

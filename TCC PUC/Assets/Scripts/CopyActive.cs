using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyActive : MonoBehaviour
{
    public GameObject from;
    public GameObject to;


    private void Update()
    {
        to.SetActive(from.activeInHierarchy);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float landscapeSize = 8f;
    public float portraitSize = 0f;

    Camera cam;


    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        GetPortraitSize();
    }

    private void Update()
    {
        if (Screen.height > Screen.width)
        {
            cam.orthographicSize = portraitSize;
        }
        else
        {
            cam.orthographicSize = landscapeSize;
        }
    }



    void GetPortraitSize()
    {
        float x, y;

        x = Screen.width;
        y = Screen.height;

        if (x > y)
        {
            portraitSize = x/y * landscapeSize;
        }
        else
        {
            portraitSize = y/x * landscapeSize;
        }
    }


}

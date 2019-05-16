using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public bool resizeByHorizontal = false;

    public float landscapeSize = 8f;
    public float portraitSize = 0f;


    public float width;
    public float height;

    [HideInInspector]public Camera camera;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        GetCamera();
        GetNewSize();
    }

    private void Start()
    {
        UpdateSize();
        
    }

    private void FixedUpdate()
    {
        //UpdateSize();
    }

    private void Update()
    {
        //UpdateSize();
    }


    void GetCamera()
    {
        camera = GetComponent<Camera>();

        if (camera == null)
        {
            camera = Camera.main;
        }
    }

    void GetNewSize()
    {
        if (resizeByHorizontal)
        {
            GetLandscapeSize();
        }
        else
        {
            GetPortraitSize();
        }
    }

    void GetPortraitSize()
    {        
        if (camera.aspect > 1)
        {
            landscapeSize = portraitSize / camera.aspect;
        }
        else
        {
            landscapeSize = portraitSize * camera.aspect;
        }
    }

    void GetLandscapeSize()
    {
        if (camera.aspect > 1)
        {
            portraitSize = landscapeSize * camera.aspect;
        }
        else
        {
            portraitSize = landscapeSize / camera.aspect;
        }
    }

    private void ReadCameraData()
    {
        height = camera.orthographicSize;
        width = height * camera.aspect;
    }

    private void UpdateSize()
    {
        if (Screen.height > Screen.width)
        {
            camera.orthographicSize = portraitSize;
        }
        else
        {
            camera.orthographicSize = landscapeSize;
        }

        ReadCameraData();
    }


}

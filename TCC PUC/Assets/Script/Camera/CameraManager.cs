using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public bool resizeByHorizontal = false;

    public float horizontalSize = 8f;
    public float verticalSize = 0f;


    public float width;
    public float height;

    [HideInInspector] public Camera camera;


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
        UpdateSize();
    }

    private void Start()
    {
        
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
        if (resizeByHorizontal)
        {
            verticalSize = horizontalSize * camera.aspect;
        }
        else
        {
            horizontalSize = verticalSize * camera.aspect;
        }
    }

    void GetLandscapeSize()
    {
        if (camera.aspect > 1)
        {
            horizontalSize = verticalSize * camera.aspect;
        }
        else
        {
            horizontalSize = verticalSize / camera.aspect;
        }
    }

    private void ReadCameraData()
    {
        if (resizeByHorizontal)
        {
            width = horizontalSize * 2;
            height = width / camera.aspect;
        }
        else
        {
            height = verticalSize * 2;
            width = height * camera.aspect;
        }
    }

    private void UpdateSize()
    {
        if (Screen.height > Screen.width)
        {
            camera.orthographicSize = verticalSize;
        }
        else
        {
            camera.orthographicSize = horizontalSize;
        }

        ReadCameraData();
    }


}

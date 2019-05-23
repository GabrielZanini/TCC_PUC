using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Camera Object")]
    public Camera camera;

    [Header("Settings")]
    public bool resizeByHorizontal = false;
    public float horizontalSize = 8f;
    public float verticalSize = 0f;
    
    [Header("Measures")]
    public float width;
    public float height;


    private void Reset()
    {
        camera = GetComponent<Camera>();
    }

    private void Awake()
    {
        GetCamera();
        ResizeCamera();
    }

    private void Start()
    {
        Debug.Log(camera.aspect + "");   
    }

    private void FixedUpdate()
    {
        //ResizeCamera();
    }

    private void Update()
    {
        //ResizeCamera();
    }

    private void OnValidate()
    {
        ResizeCamera();
    }


    void GetCamera()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
    }

    void ResizeCamera()
    {
        if (resizeByHorizontal)
        {
            verticalSize = horizontalSize / camera.aspect;
        }
        else
        {
            horizontalSize = verticalSize * camera.aspect;
        }

        width = horizontalSize * 2;
        height = verticalSize * 2;

        camera.orthographicSize = verticalSize;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public float scrollSpeed;
    public float counter = 0f;
    public bool goBack = false;


    private Vector3 startPosition;
    private float tilesScale;
    private float newPosition;
    private Camera camera;

    void Start()
    {
        if (camera == null)
        {
            camera = CameraManager.Instance.camera;
        }

        GetScale();
        MoveToBottom();

        startPosition = transform.position;
    }

    void Update()
    {
        Scroll();
    }

    private void GetScale()
    {
        tilesScale = CameraManager.Instance.width;
        transform.localScale = Vector3.one * tilesScale;
    }
    
    private void Scroll()
    {
        if (TimeController.Instance.isRewinding)
        {
            counter -= Time.deltaTime;
        }
        else
        {
            counter += Time.deltaTime;
        }

        newPosition = Mathf.Repeat(counter * scrollSpeed, tilesScale);
        transform.position = startPosition - Vector3.forward * newPosition;
    }

    private void MoveToBottom()
    {
        Vector3 newPosition = camera.transform.position;

        newPosition.x = 0;
        newPosition.y = transform.position.y;
        newPosition.z -= camera.orthographicSize - tilesScale/2f;

        transform.position = newPosition;
    } 
}

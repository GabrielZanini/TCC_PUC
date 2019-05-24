using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [Header("Camera")]
    public CameraManager camerManager;

    public float scrollSpeed;
    public float counter = 0f;
    public bool goBack = false;


    private Vector3 startPosition;
    private float tilesScale;
    private float newPosition;




    private void OnValidate()
    {
        if (camerManager != null)
        {
            SetBackground();
        }
    }

    void Awake()
    {
        AddListeners();
    }

    void Start()
    {
        SetBackground();
    }

    private void OnEnable()
    {
        SetBackground();
    }

    void Update()
    {
        Scroll();
    }

    void OnDestroy()
    {
        RemoveListeners();
    }



    void AddListeners()
    {
        if (camerManager != null)
        {
            camerManager.OnChange.AddListener(SetBackground);
        }
    }

    void RemoveListeners()
    {
        if (camerManager != null)
        {
            camerManager.OnChange.RemoveListener(SetBackground);
        }
    }


    void SetBackground()
    {
        GetScale();
        MoveToBottom();

        startPosition = transform.localPosition;
    }


    private void GetScale()
    {
        tilesScale = camerManager.width;
        transform.localScale = Vector3.one * tilesScale;
    }
    
    private void Scroll()
    {
        if (TimeController.Instance.IsRewinding)
        {
            counter -= Time.deltaTime;
        }
        else
        {
            counter += Time.deltaTime;
        }

        newPosition = Mathf.Repeat(counter * scrollSpeed, tilesScale);
        transform.localPosition = startPosition - Vector3.up * newPosition;
    }

    private void MoveToBottom()
    {
        Vector3 newPosition = Vector3.zero;
        newPosition.y -= camerManager.verticalSize - tilesScale / 2f;
        transform.localPosition = newPosition;
    } 
}

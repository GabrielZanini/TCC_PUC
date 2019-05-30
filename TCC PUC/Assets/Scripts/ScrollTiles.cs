using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimeBody))]
public class ScrollTiles : MonoBehaviour
{
    [Header("Camera")]
    public CameraManager camerManager;

    [Header("Settings")]
    public float scrollSpeed;
    public float counter = 0f;
    public bool goBack = false;

    [Header("Difficulty")]
    public bool scaleWithDificulty = true;
    public float smoothScale = 0.5f;


    private Vector3 startPosition;
    private float tilesScale;
    private float newPosition;
    private float sSpeed;
    private LevelManager level;

    [SerializeField] TimeBody timebody;



    private void Reset()
    {
        timebody = GetComponent<TimeBody>();
    }

    private void OnValidate()
    {
        timebody = GetComponent<TimeBody>();

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
        level = camerManager.Level;
    }


    private void GetScale()
    {
        tilesScale = camerManager.width;
        transform.localScale = Vector3.one * tilesScale;
    }

    private void Scroll()
    {
        counter += Time.deltaTime;

        if (scaleWithDificulty)
        {
            sSpeed = scrollSpeed * level.DifficultyModifire * smoothScale;
        }
        else
        {
            sSpeed = scrollSpeed;
        }

        newPosition = Mathf.Repeat(counter * sSpeed, tilesScale);
        transform.localPosition = startPosition - Vector3.up * newPosition;
    }

    protected virtual void MoveToBottom()
    {
        Vector3 newPosition = Vector3.zero;
        newPosition.y -= camerManager.verticalSize - tilesScale / 2f;
        transform.localPosition = newPosition;
    }
}

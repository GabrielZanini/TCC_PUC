using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance { get; private set; }


    public bool isRewinding = false;
    public string rewindButton = "Fire1";
    public float maxTimeRewind = 3f;


    public UnityEvent OnRewind;
    public UnityEvent OnRecord;


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
        if (Input.GetButtonDown(rewindButton))
        {
            StartRewind();
        }

        if (Input.GetButtonUp(rewindButton))
        {
            StoptRewind();
        }
    }

    void FixedUpdate()
    {
        if (isRewinding)
        {
            OnRewind.Invoke();
        }
        else
        {
            OnRecord.Invoke();
        }
    }


    public void StartRewind()
    {
        isRewinding = true;
        Time.timeScale = 0.5f;
    }

    public void StoptRewind()
    {
        isRewinding = false;
        Time.timeScale = 1f;
    }
}

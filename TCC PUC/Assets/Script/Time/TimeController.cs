using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance { get; private set; }


    public bool isRewinding = false;
    public bool isSlowdown = false;
    public bool canRewind = false;
    public bool canRecord = false;
    public string rewindButton = "Fire1";
    public string slowdownButton = "Fire2";
    public float maxTimeRewind = 3f;
    public float rewindTime = 3f;
    public float rewindOverloadTime = 2f;


    public UnityEvent OnRewind;
    public UnityEvent OnStartRewind;
    public UnityEvent OnStopRewind;
    public UnityEvent OnRecord;
    public UnityEvent OnOverload;
    public UnityEvent OnSlowdown;
    public UnityEvent OnStartSlowdown;
    public UnityEvent OnStopSlowdown;


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
        if (Input.GetButtonDown(rewindButton) && canRewind)
        {
            StartRewind();
        }

        if (Input.GetButtonUp(rewindButton) && canRecord)
        {
            StopRewind();
        }


        if (Input.GetButtonDown(slowdownButton))
        {
            StartSlowdown();
        }

        if (Input.GetButtonUp(slowdownButton))
        {
            StopSlowdown();
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

        if (isSlowdown)
        {
            OnSlowdown.Invoke();
        }
    }


    public void StartRewind()
    {
        isRewinding = true;
        Time.timeScale = 0.5f;
        OnStartRewind.Invoke();
    }

    public void StopRewind()
    {
        isRewinding = false;
        Time.timeScale = 1f;
        OnStopRewind.Invoke();
    }



    public void StartSlowdown()
    {
        isSlowdown = true;
        Time.timeScale = 0.5f;
        OnStartSlowdown.Invoke();
    }

    public void StopSlowdown()
    {
        isSlowdown = false;
        Time.timeScale = 1f;
        OnStopSlowdown.Invoke();
    }


}

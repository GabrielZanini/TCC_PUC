using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance { get; private set; }


    public bool isRewinding = false;
    public bool isRecording = false;
    public bool isSlowdown = false;
    public bool isOverloaded = false;

    public string rewindButton = "Fire1";
    public string slowdownButton = "Fire2";

    public float maxTimeRewind = 3f;
    public float rewindTime = 3f;
    public float overloadTime = 2f;

    public float rewindCounter = 0f;
    public float recordCounter = 0f;
    public float overloadCounter = 0f;

    [HideInInspector]public UnityEvent OnRewind;
    [HideInInspector]public UnityEvent OnStartRewind;
    [HideInInspector]public UnityEvent OnStopRewind;
    [HideInInspector]public UnityEvent OnRecord;
    [HideInInspector]public UnityEvent OnOverload;
    [HideInInspector]public UnityEvent OnSlowdown;
    [HideInInspector]public UnityEvent OnStartSlowdown;
    [HideInInspector] public UnityEvent OnStopSlowdown;


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

    void Start()
    {
        
    }

    void Update()
    {
        CheckTime();
        GetInput();
    }

    void CheckTime()
    {
        if (isRewinding || isSlowdown)
        {
            if (rewindCounter <= 0)
            {
                overloadCounter = overloadTime;

                isOverloaded = true;
                Overload();
            }
            else
            {
                rewindCounter -= Time.deltaTime;
            }
        }
        else if (overloadCounter > 0f)
        {
            overloadCounter -= Time.deltaTime;
        }
        else
        {
            isOverloaded = false;
        }

        if (!isRewinding && !isSlowdown && !isOverloaded)
        {
            if (rewindCounter >= rewindTime)
            {
                rewindCounter = rewindTime;
            }
            else
            {
                rewindCounter += Time.deltaTime;
            }
        }
    }

    void GetInput()
    {
        if (isOverloaded)
        {
            return;
        }

        if (!isSlowdown)
        {
            if (Input.GetButtonDown(rewindButton) && !isSlowdown)
            {
                StartRewind();
            }

            if (Input.GetButtonUp(rewindButton))
            {
                StopRewind();
            }
        }
        
        if (!isRewinding)
        {
            if (Input.GetButtonDown(slowdownButton))
            {
                StartSlowdown();
            }

            if (Input.GetButtonUp(slowdownButton))
            {
                StopSlowdown();
            }
        }        
    }

    void FixedUpdate()
    {
        if (isRewinding)
        {
            OnRewind.Invoke();
        }
        else if (!isOverloaded)
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
        isRecording = false;
        isSlowdown = false;
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
        isRewinding = false;
        Time.timeScale = 0.5f;
        OnStartSlowdown.Invoke();
    }

    public void StopSlowdown()
    {
        isSlowdown = false;
        Time.timeScale = 1f;
        OnStopSlowdown.Invoke();
    }

    public void Overload()
    {
        if (isRewinding)
        {
            StopRewind();
        }
        else if (isSlowdown)
        {
            StopSlowdown();
        }

        OnOverload.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance { get; private set; }

    [Header("Bools")]
    public bool isRewinding = false;
    public bool isRecording = false;
    public bool isSlowdown = false;
    public bool isOverloaded = false;

    [Header("Control Buttons")]
    public string rewindButton = "Fire1";
    public string slowdownButton = "Fire2";

    [Header("Time Parameters")]
    public float maxTimeRewind = 3f;
    public float rewindTime = 3f;
    public float overloadTime = 2f;
    [Range(0f, 1f)] public float slowdownScale = 0.5f;

    [Header("Counters")]
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
    [HideInInspector]public UnityEvent OnStopSlowdown;


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
        if (isRewinding)
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

        if (!isRewinding && !isOverloaded)
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
        if (!isOverloaded)
        {
            if (Input.GetButtonDown(rewindButton))
            {
                StartRewind();
            }

            if (Input.GetButtonUp(rewindButton))
            {
                StopRewind();
            }
        }
        

        if (Input.GetButtonDown(slowdownButton))
        {
            StartSlowdown();
        }
        else if (Input.GetButtonUp(slowdownButton))
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
        OnStartRewind.Invoke();
    }

    public void StopRewind()
    {
        isRewinding = false;
        isRecording = true;
        OnStopRewind.Invoke();
    }



    public void StartSlowdown()
    {
        isSlowdown = true;
        Time.timeScale = slowdownScale;
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

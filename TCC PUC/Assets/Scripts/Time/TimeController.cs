using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeController : MonoBehaviour
{
    private List<TimeBody> timebodys = new List<TimeBody>();

    [Header("Bools")]
    [SerializeField] bool isRewinding = false;
    public bool IsRewinding {
        get { return isRewinding; }
        private set { isRewinding = value; }
    }
    [SerializeField] bool isRecording = false;
    public bool IsRecording {
        get { return isRecording; }
        private set { isRecording = value; }
    }
    [SerializeField] bool isSlowdown = false;
    public bool IsSlowdown {
        get { return isSlowdown; }
        private set { isSlowdown = value; }
    }
    [SerializeField] bool isOverloaded = false;
    public bool IsOverloaded {
        get { return isOverloaded; }
        private set { isOverloaded = value; }
    }


    [Header("Control Buttons")]
    public string rewindButton = "Fire1";
    public string slowdownButton = "Fire2";
    [SerializeField] MobileButton mobileRewindButton;
    [SerializeField] PlayerInput playerInput;


    [Header("Time Parameters")]
    public float rewindTime = 3f;
    [SerializeField] int maxPointsInTime;
    public int MaxPointsInTime {
        get { return maxPointsInTime; }
        private set { maxPointsInTime = value; }
    }
    [SerializeField] int currentPointInTime = 0;
    public int CurrentPointInTime {
        get { return currentPointInTime; }
        set { currentPointInTime = value; }
    }
    [SerializeField] int lastPointInTime = 0;
    public int LastPointInTime {
        get { return lastPointInTime; }
        set { lastPointInTime = value; }
    }
    [SerializeField] int pointsInTimeCount = 0;
    public int PointsInTimeCount {
        get { return pointsInTimeCount; }
        private set { pointsInTimeCount = value; }
    }
    public int TimebodysCount {
        get { return timebodys.Count; }
    }
    private bool isFirstSlowdown = true;

    public float overloadTime = 2f;
    [Range(0f, 1f)] public float slowdownScale = 0.5f;

    [Header("Counters")]
    [SerializeField] float rewindCounter = 0f;
    [SerializeField] float recordCounter = 0f;
    [SerializeField] float overloadCounter = 0f;

    

    // Events
    [HideInInspector] public UnityEvent OnRewind;
    [HideInInspector] public UnityEvent OnStartRewind;
    [HideInInspector] public UnityEvent OnStopRewind;
    [HideInInspector] public UnityEvent OnRecord;
    [HideInInspector] public UnityEvent OnOverload;
    [HideInInspector] public UnityEvent OnSlowdown;
    [HideInInspector] public UnityEvent OnStartSlowdown;
    [HideInInspector] public UnityEvent OnStopSlowdown;



    void Awake()
    {
        MaxPointsInTime = (int)Mathf.Round(rewindTime / Time.fixedDeltaTime);
    }

    void OnEnable()
    {
        AddListener();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (GameManager.Instance.Level.State == LevelState.Playing)
        {
            CheckTime();
            GetInput();
        }
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.Level.State == LevelState.Playing)
        {
            if (isRewinding)
            {
                Rewind();
            }
            else if (!isOverloaded)
            {
                Record();
            }

            if (isSlowdown)
            {
                Slowdown();
            }
        }        
    }
    
    private void OnDisable()
    {
        RemoveListener();
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
                if (!GameManager.Instance.IsMobile)
                {
                    rewindCounter -= Time.deltaTime;
                }
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
        

        if ((Input.GetButtonDown(slowdownButton) || !playerInput.HasTouch) && GameManager.Instance.Level.State == LevelState.Playing)
        {
            StartSlowdown();
        }
        else if (Input.GetButtonUp(slowdownButton) || playerInput.HasTouch || !(GameManager.Instance.Level.State == LevelState.Playing))
        {
            StopSlowdown();
        }
    }

    void UpdateParameters()
    {
        PointsInTimeCount = timebodys[0].pointsInTime.Count;
        LastPointInTime = CurrentPointInTime;
        CurrentPointInTime = pointsInTimeCount;
    }

    void AddListener()
    {
        mobileRewindButton.OnPress.AddListener(StartRewind);
        mobileRewindButton.OnRelease.AddListener(StopRewind);

        GameManager.Instance.Level.OnStart.AddListener(ClearList);
        GameManager.Instance.Level.OnStop.AddListener(ClearList);
    }

    void RemoveListener()
    {
        mobileRewindButton.OnPress.RemoveListener(StartRewind);
        mobileRewindButton.OnRelease.RemoveListener(StopRewind);

        GameManager.Instance.Level.OnStart.RemoveListener(ClearList);
        GameManager.Instance.Level.OnStop.RemoveListener(ClearList);
    }


    // Slowdown

    public void StartRewind()
    {
        isRewinding = true;
        isRecording = false;
        OnStartRewind.Invoke();
    }

    public void StopRewind()
    {
        //Debug.Log("TimeController - StopRewind");

        isRewinding = false;
        isRecording = true;

        if (GameManager.Instance.IsMobile)
        {
            ClearList();
        }

        OnStopRewind.Invoke();
    }
    
    public void Rewind()
    {
        if (GameManager.Instance.IsMobile)
        {
            RewindMobile();
        }
        else
        {
            RewindDefault();
        }
    }

    public void RewindDefault()
    {
        for (int i = 0; i < timebodys.Count; i++)
        {
            timebodys[i].Rewind();
        }

        PointsInTimeCount--;
        OnRewind.Invoke();
    }
    
    public void RewindMobile()
    {
        for (int i = 0; i < timebodys.Count; i++)
        {
            timebodys[i].SetPointInTime(PointsInTimeCount - CurrentPointInTime);
        }
    }

    
    // Record

    public void Record()
    {
        for (int i=0; i<timebodys.Count; i++)
        {
            timebodys[i].Record();
        }

        if (PointsInTimeCount < MaxPointsInTime)
        {
            UpdateParameters();
        }

        OnRecord.Invoke();
    }


    // Slowdown

    public void StartSlowdown()
    {
        isFirstSlowdown = false;

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

    public void Slowdown()
    {
        OnSlowdown.Invoke();
    }


    // Overload

    public void Overload()
    {
        if (isRewinding)
        {
            StopRewind();
        }

        OnOverload.Invoke();
    }


    //Time Bodys

    public void AddTimebody(TimeBody timebody)
    {
        timebodys.Add(timebody);
    }

    public void RemoveTimebody(TimeBody timebody)
    {
        timebodys.Remove(timebody);
    }

    public void ClearList()
    {
        //Debug.Log("TimeController - ClearList");

        for (int i = 0; i < timebodys.Count; i++)
        {
            timebodys[i].ShortList(CurrentPointInTime);
        }

        UpdateParameters();
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeController : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameManager gameManager;
    public GameManager GameManager {
        get { return gameManager; }
        private set { gameManager = value; }
    }

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
    [SerializeField] List<MobileButton> mobileRewindButtons = new List<MobileButton>();
    [SerializeField] PlayerInput playerInput;


    [Header("Time Parameters")]
    [SerializeField][Range(0, 60)] int stepsPerSecond = 30;
    public int StepsPerSecond {
        get { return stepsPerSecond; }
        private set { stepsPerSecond = value; }
    }
    [SerializeField] float timeStep = 0.2f;
    [SerializeField] float rewindTime = 3f;
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
    private float stepTimer = 0f;

    public float overloadTime = 2f;
    [Range(0f, 1f)] public float slowdownScale = 0.5f;

    [Header("Counters")]
    [SerializeField] float rewindCounter = 0f;
    [SerializeField] float recordCounter = 0f;
    [SerializeField] float overloadCounter = 0f;
    [SerializeField] int stepsCounter = 0;

    [Header("TimeBodys")]
    private List<TimeBody> timebodys = new List<TimeBody>();


    // Events
    [HideInInspector] public UnityEvent OnRewind;
    [HideInInspector] public UnityEvent OnStartRewind;
    [HideInInspector] public UnityEvent OnStopRewind;
    [HideInInspector] public UnityEvent OnRecord;
    [HideInInspector] public UnityEvent OnOverload;
    [HideInInspector] public UnityEvent OnSlowdown;
    [HideInInspector] public UnityEvent OnStartSlowdown;
    [HideInInspector] public UnityEvent OnStopSlowdown;




    private void Reset()
    {
        GameManager = GetComponentInParent<GameManager>();
    }

    private void OnValidate()
    {
        if (StepsPerSecond > 0)
        {
            timeStep = 1f / StepsPerSecond;
            MaxPointsInTime = (int)Mathf.Round(rewindTime / timeStep);
        }        
    }

    void Awake()
    {
        OnValidate();
    }

    void OnEnable()
    {

    }

    void Start()
    {
        AddListeners();
    }

    void Update()
    {
        if (GameManager.Instance.Level.State == LevelState.Playing && StepsPerSecond > 0)
        {
            CheckTime();
            GetInput();
            TimeLoop();
        }
    }

    void FixedUpdate()
    {
        //TimeStep();   
    }

    private void OnDisable()
    {
        RemoveListeners();
    }


    void AddListeners()
    {
        foreach (var mobileButton in mobileRewindButtons)
        {
            mobileButton.OnPress.AddListener(StartRewind);
            mobileButton.OnRelease.AddListener(StopRewind);
        }

        GameManager.Level.OnMenu.AddListener(StopSlowdown);
        GameManager.Level.OnStart.AddListener(ClearList);
        GameManager.Level.OnStop.AddListener(ClearList);
        GameManager.Level.OnStop.AddListener(StopSlowdown);
        GameManager.Level.OnRestart.AddListener(ClearList);
    }

    void RemoveListeners()
    {
        foreach (var mobileButton in mobileRewindButtons)
        {
            mobileButton.OnPress.RemoveListener(StartRewind);
            mobileButton.OnRelease.RemoveListener(StopRewind);
        }

        GameManager.Level.OnMenu.RemoveListener(StopSlowdown);
        GameManager.Level.OnStart.RemoveListener(ClearList);
        GameManager.Level.OnStop.RemoveListener(ClearList);
        GameManager.Level.OnStop.RemoveListener(StopSlowdown);
        GameManager.Level.OnRestart.RemoveListener(ClearList);
    }


    void CheckTime()
    {
        if (!GameManager.Instance.IsMobile)
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

    void TimeLoop()
    {
        if (stepTimer <= 0f)
        {
            stepTimer = timeStep;
            TimeStep();
        }
        else
        {
            stepTimer -= Time.deltaTime;
        }
    }

    void TimeStep()
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

            stepsCounter += 1;
        }
    }

    void UpdateParameters()
    {
        PointsInTimeCount = timebodys[0].pointsInTime.Count;
        LastPointInTime = CurrentPointInTime;
        CurrentPointInTime = pointsInTimeCount;
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
            ShortList();
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
        currentPointInTime = 1;
        ShortList();
    }


    public void ShortList()
    {
        for (int i = 0; i < timebodys.Count; i++)
        {
            timebodys[i].ShortList(CurrentPointInTime);
        }

        UpdateParameters();
    }



}

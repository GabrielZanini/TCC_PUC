using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [Header("Bools")]
    [SerializeField] bool hasStarted = false;
    public bool HasStarted {
        get { return hasStarted; }
        private set { hasStarted = value; }
    }

    [SerializeField] bool isPlaying = false;
    public bool IsPlaying {
        get { return isPlaying; }
        private set { isPlaying = value; }
    }

    [SerializeField] bool isPaused = false;
    public bool IsPaused {
        get { return isPaused; }
        private set { isPaused = value; }
    }

    [SerializeField] bool isFinished = false;
    public bool IsFinished {
        get { return isFinished; }
        private set { isFinished = value; }
    }


    [Header("Difficulty")]
    [SerializeField] float startingDifficulty = 1f;
    [SerializeField] float difficultySpeed = 0.01f;
    [SerializeField] float difficultyModifire = 1f;
    public float DifficultyModifire {
        get { return difficultyModifire; }
        private set { difficultyModifire = value; }
    }
    [SerializeField] bool increasingDifficulty = false;
    public bool IncreasingDifficulty {
        get { return increasingDifficulty; }
        private set { increasingDifficulty = value; }
    }

    [HideInInspector] public UnityEvent OnBeforeStart;
    [HideInInspector] public UnityEvent OnStart;
    [HideInInspector] public UnityEvent OnPause;
    [HideInInspector] public UnityEvent OnContinue;
    [HideInInspector] public UnityEvent OnStop;
    [HideInInspector] public UnityEvent OnRestart;
    [HideInInspector] public UnityEvent OnFinish;



    void Start()
    {
        BeforeStart();
    }

    void Update()
    {
        ReadInput();
        IncreaseDifficulty();
    }



    void ReadInput()
    {
        if (GameManager.Instance.IsMobile && !hasStarted)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StartLevel();
            }
        }
    }

    void IncreaseDifficulty()
    {
        if (IsPlaying && IncreasingDifficulty)
        {
            DifficultyModifire += difficultySpeed * Time.deltaTime;
        }
    }

    private void RestarDifficulty()
    {
        DifficultyModifire = startingDifficulty;
    }

    void BeforeStart()
    {
        //Debug.Log("Level - Before Start");

        HasStarted = false;
        IsPlaying = false;
        IsPaused = false;
        IsFinished = false;

        OnBeforeStart.Invoke();
    }
    
    void StartLevel()
    {
        //Debug.Log("Level - Start");

        HasStarted = true;
        IsPlaying = true;

        RestarDifficulty();

        OnStart.Invoke();
    }

    void FinishLevel()
    {
        //Debug.Log("Level - Finish");

        IsPlaying = false;
        IsFinished = true;

        OnFinish.Invoke();
    }

    public void Pause()
    {
        //Debug.Log("Level - Pause");

        IsPlaying = false;
        IsPaused = true;

        Time.timeScale = 0f;

        OnPause.Invoke();
    }

    public void Continue()
    {
        //Debug.Log("Level - Continue");

        IsPlaying = true;
        IsPaused = false;

        Time.timeScale = 1f;

        OnContinue.Invoke();
    }

    public void Restart()
    {
        //Debug.Log("Level - Restart");

        OnRestart.Invoke();

        BeforeStart();
    }
    
    public void Stop()
    {
        //Debug.Log("Level - Stop");

        IsPlaying = false;
        Time.timeScale = 1f;

        OnStop.Invoke();
    }



    private void OnApplicationPause(bool pause)
    {
        if (IsPlaying)
        {
            Pause();
        }        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
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

    [SerializeField] bool isFinished = false;
    public bool IsFinished {
        get { return isFinished; }
        private set { isFinished = value; }
    }


    [HideInInspector] public UnityEvent OnStart;
    [HideInInspector] public UnityEvent OnFinish;
    [HideInInspector] public UnityEvent OnRestart;
    [HideInInspector] public UnityEvent OnStop;



    void Start()
    {
        
    }

    void Update()
    {
        ReadInput();
    }


    void ReadInput()
    {
        if (GameManager.Instance.IsMobile && !hasStarted)
        {
            if (Input.GetKey(KeyCode.Mouse0) || Input.touchCount > 0)
            {
                StartLevel();
            }
        }
    }


    void StartLevel()
    {
        HasStarted = true;
        IsPlaying = true;
        OnStart.Invoke();
    }

    void FinishLevel()
    {
        IsPlaying = false;
        IsFinished = true;
        OnFinish.Invoke();
    }
    

    public void Restart()
    {
        IsPlaying = true;
        IsFinished = false;

        OnStart.Invoke();
        OnRestart.Invoke();
    }

    public void Stop()
    {
        IsPlaying = false;
        OnStop.Invoke();
    }
}

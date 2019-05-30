using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameManager gameManager;
    public GameManager GameManager {
        get { return gameManager; }
        private set { gameManager = value; }
    }

    [Header("Level State")]
    [SerializeField] LevelState state = LevelState.Menu;
    public LevelState State {
        get { return state; }
        private set { state = value; }
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

    [Header("Score")]
    [SerializeField] ScoreManager score;
    public ScoreManager Score {
        get { return score; }
        private set { score = value; }
    }

    [HideInInspector] public UnityEvent OnMenu;
    [HideInInspector] public UnityEvent OnStart;
    [HideInInspector] public UnityEvent OnPause;
    [HideInInspector] public UnityEvent OnContinue;
    [HideInInspector] public UnityEvent OnStop;
    [HideInInspector] public UnityEvent OnRestart;
    [HideInInspector] public UnityEvent OnFinish;



    void Start()
    {
        Menu();
    }

    void Update()
    {
        //ReadInput();
        IncreaseDifficulty();
    }



    void ReadInput()
    {
        if (GameManager.Instance.IsMobile && state == LevelState.Menu)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StartLevel();
            }
        }
    }

    void IncreaseDifficulty()
    {
        if (state == LevelState.Playing && IncreasingDifficulty)
        {
            DifficultyModifire += difficultySpeed * Time.deltaTime;
        }
    }

    private void RestarDifficulty()
    {
        DifficultyModifire = startingDifficulty;
    }

    public void Menu()
    {
        state = LevelState.Menu;
        OnMenu.Invoke();
    }
    
    public void StartLevel()
    {
        state = LevelState.Playing;
        RestarDifficulty();
        OnStart.Invoke();
    }

    void FinishLevel()
    {
        state = LevelState.Finished;
        OnFinish.Invoke();
    }

    public void Pause()
    {
        state = LevelState.Paused;
        Time.timeScale = 0f;
        OnPause.Invoke();
    }

    public void Continue()
    {
        state = LevelState.Playing;
        Time.timeScale = 1f;
        OnContinue.Invoke();
    }

    public void Restart()
    {
        StartLevel();
        OnRestart.Invoke();
    }
    
    public void Stop()
    {
        state = LevelState.Stoped;
        OnStop.Invoke();
    }



    private void OnApplicationPause(bool pause)
    {
        if (state == LevelState.Playing)
        {
            Pause();
        }        
    }
}

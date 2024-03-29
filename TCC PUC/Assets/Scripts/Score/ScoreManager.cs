﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [Header("Level")]
    [SerializeField] LevelManager level;
    public LevelManager Level {
        get { return level; }
        private set { level = value; }
    }



    [SerializeField] int highest = 0;
    public int Highest {
        get { return highest; }
        private set { highest = value; }
    }

    [SerializeField] int current = 0;
    public int Current {
        get { return current; }
        private set { current = value; }
    }



    [HideInInspector] public UnityEvent OnChange;
    [HideInInspector] public UnityEvent OnNewHighest;



    private void Awake()
    {
        Load();
    }

    private void Start()
    {
        AddListener();
    }

    private void OnDestroy()
    {
        RemoveListener();
        Save();
    }



    // Listeners

    void AddListener()
    {
        GameManager.Instance.Level.OnStart.AddListener(ClearScore);
    }

    void RemoveListener()
    {
        GameManager.Instance.Level.OnStart.RemoveListener(ClearScore);
    }


    // Points 

    void ClearScore()
    {
        Current = 0;
        OnChange.Invoke();
    }

    public void Add(int points)
    {
        Current += (int) (points * GameManager.Instance.Level.DifficultyModifire);
        UpdateScore();
        OnChange.Invoke();
    }

    public void UpdateScore()
    {
        if (Current > highest)
        {
            Highest = Current;
            Save();
            OnNewHighest.Invoke();
        }
    }


    // Save and Load

    void Save()
    {
        PlayerPrefs.SetInt("HighestScore", Highest);
    }

    void Load()
    {
        Highest = PlayerPrefs.GetInt("HighestScore");
    }

    private void OnApplicationPause(bool pause)
    {
        Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}

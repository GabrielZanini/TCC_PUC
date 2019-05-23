using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewindBar : MonoBehaviour
{
    public Slider fullBar;
    public Slider rewindBar;

    public MobileButton button;

    private void OnEnable()
    {
        AddListener();
    }

    void Start()
    {
        fullBar.maxValue = TimeController.Instance.MaxPointsInTime;
    }

    private void OnDisable()
    {
        RemoveListener();
    }

    void Update()
    {
        UpdateBars();
    }



    void AddListener()
    {
        GameManager.Instance.Level.OnStart.AddListener(ClearBars);
        GameManager.Instance.Level.OnStop.AddListener(ClearBars);
    }

    void RemoveListener()
    {
        GameManager.Instance.Level.OnStart.RemoveListener(ClearBars);
        GameManager.Instance.Level.OnStop.RemoveListener(ClearBars);
    }



    void ClearBars()
    {
        fullBar.value = 1;
        rewindBar.value = 1;
    }
        
    void UpdateBars()
    {
        fullBar.value = TimeController.Instance.PointsInTimeCount;
        rewindBar.maxValue = fullBar.value;

        if (button.Hold)
        {
            TimeController.Instance.CurrentPointInTime = (int)rewindBar.value;
        }
        else
        {
            rewindBar.value = TimeController.Instance.CurrentPointInTime;
        }
    }

}

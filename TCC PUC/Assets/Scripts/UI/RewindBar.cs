using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewindBar : MonoBehaviour
{
    [Header("Time Controller")]
    public TimeController timeController;

    public Slider fullBar;
    public Slider rewindBar;

    public MobileButton button;
    


    void Start()
    {
        fullBar.maxValue = timeController.MaxPointsInTime;


        AddListener();
    }

    private void OnDestroy()
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
        fullBar.value = timeController.PointsInTimeCount;
        rewindBar.maxValue = fullBar.value;

        if (button.Hold)
        {
            timeController.CurrentPointInTime = (int)rewindBar.value;
        }
        else
        {
            rewindBar.value = timeController.CurrentPointInTime;
        }
    }

}

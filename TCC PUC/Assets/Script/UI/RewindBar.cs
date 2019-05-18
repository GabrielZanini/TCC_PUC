using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewindBar : MonoBehaviour
{
    public Slider fullBar;
    public Slider rewindBar;

    public MobileButton button;


    void Start()
    {
        fullBar.maxValue = TimeController.Instance.MaxPointsInTime;
    }



    void Update()
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteToggle : MonoBehaviour
{

    public Toggle mute;



    void Start()
    {
        AddListener();
        UpdateMute();
    }

    private void OnDestroy()
    {
        RemoveListener();
    }



    // Listeners

    void AddListener()
    {
        GameManager.Instance.Audio.OnMute.AddListener(GetMute);
    }

    void RemoveListener()
    {
        GameManager.Instance.Audio.OnMute.RemoveListener(GetMute);
    }



    // Control

    public void UpdateMute()
    {
        GameManager.Instance.Audio.Mute = mute.isOn;
    }

    void GetMute()
    {
        mute.isOn = GameManager.Instance.Audio.Mute;
    }

}

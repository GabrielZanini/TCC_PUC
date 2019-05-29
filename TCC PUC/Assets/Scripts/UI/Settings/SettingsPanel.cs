using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [Header("Volume")]
    public Slider music;
    public Slider sfx;
    public Slider voice;

    [Header("Time Bar")]
    public Toggle right;
    public Toggle left;
    public Toggle bottom;

    [Header("Vibration")]
    public Toggle vibraterion;

    [Header("Canvas")]
    public GameplayPanel gameplayPanel;



    private void Start()
    {
        GetVolume();
        GetBarPosition();
        GetVibration();
    }



    public void UpdateVolumeMusic()
    {
        GameManager.Instance.Audio.SetVolumeMusic(music.value);
    }

    public void UpdateVolumeSfx()
    {
        GameManager.Instance.Audio.SetVolumeSfx(sfx.value);
    }

    public void UpdateVolumeVoice()
    {
        GameManager.Instance.Audio.SetVolumeVoice(voice.value);
    }

    public void UpdateVibration()
    {
        GameManager.Instance.UseVibration = vibraterion.isOn;
    }

    void GetVolume()
    {
        music.value = GameManager.Instance.Audio.MusicVolume;
        sfx.value = GameManager.Instance.Audio.SfxVolume;
        voice.value = GameManager.Instance.Audio.VoiceVolume;
    }

    void GetVibration()
    {
        vibraterion.isOn = GameManager.Instance.UseVibration;
    }



    void GetBarPosition()
    {
        if (gameplayPanel.Position == Direction.Right)
        {
            right.isOn = true;
        }
        else if (gameplayPanel.Position == Direction.Left)
        {
            left.isOn = true;
        }
        else if (gameplayPanel.Position == Direction.Down)
        {
            bottom.isOn = true;
        }
    }


    public void ChangeSliderPosition()
    {
        if (right.isOn)
        {
            gameplayPanel.SetSlideRight();
        }
        else if(left.isOn)
        {
            gameplayPanel.SetSlideLeft();
        }
        else if(bottom.isOn)
        {
            gameplayPanel.SetSlideBottom();
        }
    }
}

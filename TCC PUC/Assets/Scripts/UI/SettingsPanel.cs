using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public Slider music;
    public Slider sfx;
    public Slider voice;

    public Toggle vibraterion;


    private void Start()
    {
        GetVolume();
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
}

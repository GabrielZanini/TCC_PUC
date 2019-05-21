using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioController : MonoBehaviour
{
    [Header("Volume")]
    [Range(0f, 1f)]
    [SerializeField] float musicVolume = 1f;
    public float MusicVolume {
        get { return musicVolume; }
        private set { musicVolume = value; OnChange(); }
    }

    [Range(0f, 1f)]
    [SerializeField] float sfxVolume = 1f;
    public float SfxVolume {
        get { return sfxVolume; }
        private set { sfxVolume = value; OnChange(); }
    }

    [Range(0f, 1f)]
    [SerializeField] float voiceVolume = 1f;
    public float VoiceVolume {
        get { return voiceVolume; }
        private set { voiceVolume = value; OnChange(); }
    }


    [Header("Lists")]
    [SerializeField] List<AudioManager> musics = new List<AudioManager>();
    [SerializeField] List<AudioManager> sfxs = new List<AudioManager>();
    [SerializeField] List<AudioManager> voices = new List<AudioManager>();


    [HideInInspector] public UnityEvent OnChangeVolume;



    private void Awake()
    {
        Load();
    }

    private void OnValidate()
    {
        OnChange();
    }

    private void OnApplicationPause(bool pause)
    {
        Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }



    // Volume

    public float GetVolume(Enums.AudioType type)
    {
        if (type == Enums.AudioType.Music)
        {
            return MusicVolume;
        }
        else if (type == Enums.AudioType.SFX)
        {
            return SfxVolume;
        }
        else if (type == Enums.AudioType.Voice)
        {
            return VoiceVolume;
        }
        else
        {
            Debug.LogError("Error AudioController.GetVolume - audio.type doesn't exist.");
            return 0;
        }
    }

    public void SetVolumeMusic(float volume)
    {
        musicVolume = volume;
        OnChange();
    }

    public void SetVolumeSfx(float volume)
    {
        sfxVolume = volume;
        OnChange();
    }

    public void SetVolumeVoice(float volume)
    {
        voiceVolume = volume;
        OnChange();
    }



    // Lists

    public void Add(AudioManager audio)
    {
        if (audio.type == Enums.AudioType.Music)
        {
            musics.Add(audio);
        }
        else if (audio.type == Enums.AudioType.SFX)
        {
            sfxs.Add(audio);
        }
        else if (audio.type == Enums.AudioType.Voice)
        {
            voices.Add(audio);
        }
        else
        {
            Debug.LogError("Error AudioController.Add - audio.type doesn't exist.");
        }
    }

    public void Remove(AudioManager audio)
    {
        if (audio.type == Enums.AudioType.Music)
        {
            musics.Remove(audio);
        }
        else if (audio.type == Enums.AudioType.SFX)
        {
            sfxs.Remove(audio);
        }
        else if (audio.type == Enums.AudioType.Voice)
        {
            voices.Remove(audio);
        }
        else
        {
            Debug.LogError("Error AudioController.Remove - audio.type doesn't exist.");
        }
    }



    // Event

    void OnChange()
    {
        Save();
        OnChangeVolume.Invoke();
    }



    // Save / Load

    void Save()
    {
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
        PlayerPrefs.SetFloat("SfxVolume", SfxVolume);
        PlayerPrefs.SetFloat("VoiceVolume", VoiceVolume);
    }

    void Load()
    {
        if (PlayerPrefs.HasKey("Vibrate"))
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            musicVolume = 1;
        }

        if (PlayerPrefs.HasKey("SfxVolume"))
        {
            sfxVolume = PlayerPrefs.GetFloat("SfxVolume");
        }
        else
        {
            sfxVolume = 1;
        }

        if (PlayerPrefs.HasKey("VoiceVolume"))
        {
            voiceVolume = PlayerPrefs.GetFloat("VoiceVolume");
        }
        else
        {
            voiceVolume = 1;
        }

    }


}

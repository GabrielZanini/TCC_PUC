using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioController : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameManager gameManager;
    public GameManager GameManager {
        get { return gameManager; }
        private set { gameManager = value; }
    }

    [Header("Volume")]
    [Range(0f, 1f)]
    [SerializeField] float musicVolume = 1f;
    public float MusicVolume {
        get { return musicVolume; }
        private set { musicVolume = value; ChangeVolume(); }
    }

    [Range(0f, 1f)]
    [SerializeField] float sfxVolume = 1f;
    public float SfxVolume {
        get { return sfxVolume; }
        private set { sfxVolume = value; ChangeVolume(); }
    }

    [Range(0f, 1f)]
    [SerializeField] float voiceVolume = 1f;
    public float VoiceVolume {
        get { return voiceVolume; }
        private set { voiceVolume = value; ChangeVolume(); }
    }


    [Header("Mute")]
    [SerializeField] bool mute = false;
    public bool Mute {
        get { return mute; }
        set { mute = value; ChangeMute(); }
    }


    [Header("Lists")]
    [SerializeField] List<AudioManager> musics = new List<AudioManager>();
    [SerializeField] List<AudioManager> sfxs = new List<AudioManager>();
    [SerializeField] List<AudioManager> voices = new List<AudioManager>();


    [HideInInspector] public UnityEvent OnChangeVolume;
    [HideInInspector] public UnityEvent OnMute;



    private void Awake()
    {
        Load();
    }

    private void OnValidate()
    {
        ChangeVolume();
        ChangeMute();
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

    public float GetVolume(AudioType type)
    {
        if (type == AudioType.Music)
        {
            return MusicVolume;
        }
        else if (type == AudioType.SFX)
        {
            return SfxVolume;
        }
        else if (type == AudioType.Voice)
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
        ChangeVolume();
    }

    public void SetVolumeSfx(float volume)
    {
        sfxVolume = volume;
        ChangeVolume();
    }

    public void SetVolumeVoice(float volume)
    {
        voiceVolume = volume;
        ChangeVolume();
    }



    // Lists

    public void Add(AudioManager audio)
    {
        if (audio.type == AudioType.Music)
        {
            musics.Add(audio);
        }
        else if (audio.type == AudioType.SFX)
        {
            sfxs.Add(audio);
        }
        else if (audio.type == AudioType.Voice)
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
        if (audio.type == AudioType.Music)
        {
            musics.Remove(audio);
        }
        else if (audio.type == AudioType.SFX)
        {
            sfxs.Remove(audio);
        }
        else if (audio.type == AudioType.Voice)
        {
            voices.Remove(audio);
        }
        else
        {
            Debug.LogError("Error AudioController.Remove - audio.type doesn't exist.");
        }
    }



    // Event

    void ChangeVolume()
    {
        Save();
        OnChangeVolume.Invoke();
    }

    void ChangeMute()
    {
        Save();
        OnMute.Invoke();
    }



    // Save / Load

    void Save()
    {
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
        PlayerPrefs.SetFloat("SfxVolume", SfxVolume);
        PlayerPrefs.SetFloat("VoiceVolume", VoiceVolume);
        PlayerPrefs.SetInt("Mute", Mute ? 1 : 0);
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


        if (PlayerPrefs.HasKey("Mute"))
        {
            mute = PlayerPrefs.GetInt("Mute") == 1;
        }
        else
        {
            mute = false;
        }
    }


}

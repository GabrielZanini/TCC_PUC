using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [Header("Source")]
    public AudioSource source;
    public AudioClip clip;

    [Header("Settings")]
    public AudioType type = AudioType.Music;
    public bool playAtStart = true;
    public bool loop = false;
    public bool stopOnRewind = true;

    [SerializeField] bool mute = false;


    [Range(0f, 1f)]
    [SerializeField] float volume = 1f;

    [Header("Counter")]
    public float counter = 0f;

    [SerializeField]
    TimeBody timebody;


    private void Reset()
    {
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        timebody = GetComponent<TimeBody>();
        
        if (timebody != null)
        {
            timebody.scriptsToDisable.Add(this);
        }
    }

    private void Awake()
    {
        
    }

    private void Start()
    {
        GameManager.Instance.Audio.Add(this);
        
        AddListener();
        UpdateSource();
        

        if (playAtStart)
        {
            Play();
            
        }
        else
        {
            Stop();
        }
    }

    private void Update()
    {
        if (clip.length > counter)
        {
            counter += Time.deltaTime;
        }
        else
        {
            counter = 0f;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.Audio.Remove(this);
        RemoveListener();
    }

    private void OnValidate()
    {
        UpdateSource();
    }


    // Listeners

    void AddListener()
    {
        GameManager.Instance.Audio.OnChangeVolume.AddListener(UpdateVolume);
        GameManager.Instance.Audio.OnMute.AddListener(UpdateMute);

        if (timebody != null)
        {
            timebody.OnSpawn.AddListener(Replay);
            timebody.OnActivate.AddListener(Replay);

            if (stopOnRewind)
            {
                timebody.Controller.OnStartRewind.AddListener(Stop);
                timebody.Controller.OnStopRewind.AddListener(Play);
            }
        }        
    }

    void RemoveListener()
    {
        GameManager.Instance.Audio.OnChangeVolume.RemoveListener(UpdateVolume);
        GameManager.Instance.Audio.OnMute.RemoveListener(UpdateMute);

        if (timebody != null)
        {
            timebody.OnSpawn.RemoveListener(Replay);
            timebody.OnActivate.RemoveListener(Replay);

            if (stopOnRewind)
            {
                timebody.Controller.OnStartRewind.RemoveListener(Stop);
                timebody.Controller.OnStopRewind.RemoveListener(Play);
            }
        }        
    }



    // Source

    void UpdateSource()
    {
        source.clip = clip;
        source.loop = loop;

        UpdateMute();
        UpdateVolume();
    }

    void UpdateVolume()
    {
        if (GameManager.Instance != null)
        {
            source.volume = volume * GameManager.Instance.Audio.GetVolume(type);
        }
        else
        {
            source.volume = volume;
        }
    }

    void UpdateMute()
    {
        if (GameManager.Instance != null)
        {
            source.mute = mute || GameManager.Instance.Audio.Mute;
        }
        else
        {
            source.mute = mute;
        }
    }



    // Control

    public void Play()
    {
        source.time = counter;
        source.Play();
    }

    public void Replay()
    {
        source.time = 0;
        source.Play();
    }

    void Stop()
    {
        source.Stop();
    }

    public void SetTime(float time)
    {
        if (time <= clip.length)
        {
            source.time = time;
        }
    }


}

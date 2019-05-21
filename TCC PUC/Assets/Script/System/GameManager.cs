using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }



    [SerializeField] LevelManager level;
    public LevelManager Level {
        get { return level; }
        private set { level = value; }
    }

    [SerializeField] PoolsManager pools;
    public PoolsManager Pools {
        get { return pools; }
        private set { pools = value; }
    }

    [SerializeField] AudioController audio;
    public AudioController Audio {
        get { return audio; }
        private set { audio = value; }
    }

    [SerializeField] ShipManager player;
    public ShipManager Player {
        get { return player; }
        private set { player = value; }
    }

    [SerializeField] ScoreManager score;
    public ScoreManager Score {
        get { return score; }
        private set { score = value; }
    }



    [Header("Settings")]

    [SerializeField] Enums.Platform platform;
    public Enums.Platform Platform {
        get { return platform; }
        private set { platform = value; }
    }

    [SerializeField] bool useVibration = true;
    public bool UseVibration {
        get { return useVibration; }
        set { useVibration = value; OnSetVibration(); }
    }
    
    public bool IsMobile {
        get {
            return Platform == Enums.Platform.Android || Platform == Enums.Platform.Iphone;
        }
    }


    [HideInInspector] public UnityEvent OnVibrationChange;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        //Debug.Log("GameManager.Instance");

        Load();
        GetPlatform();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }



    void OnSetVibration()
    {
        Save();
        OnVibrationChange.Invoke();
    }


    void GetPlatform()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                Platform = Enums.Platform.Android;
                break;

            case RuntimePlatform.IPhonePlayer:
                Platform = Enums.Platform.Iphone;
                break;

            case RuntimePlatform.WindowsPlayer:
                Platform = Enums.Platform.Windows;
                break;

            case RuntimePlatform.LinuxPlayer:
                Platform = Enums.Platform.Lunix;
                break;

            case RuntimePlatform.OSXPlayer:
                Platform = Enums.Platform.Mac;
                break;

            case RuntimePlatform.PS4:
                Platform = Enums.Platform.Playstation;
                break;

            case RuntimePlatform.XboxOne:
                Platform = Enums.Platform.Xbox;
                break;

            case RuntimePlatform.Switch:
                Platform = Enums.Platform.Switch;
                break;

            default:
                break;

        }
    }


    void Save()
    {
        PlayerPrefs.SetInt("Vibrate", useVibration ? 1 : 0);
    } 

    void Load()
    {
        if (PlayerPrefs.HasKey("Vibrate"))
        {
            useVibration = PlayerPrefs.GetInt("Vibrate") == 1;
        }
    }
}

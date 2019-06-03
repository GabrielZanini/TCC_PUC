using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }



    [Header("Controllers and Managers")]

    [SerializeField] TimeController timeController;
    public TimeController TimeController {
        get { return timeController; }
        private set { timeController = value; }
    }

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



    [Header("Settings")]

    [SerializeField] Platform platform;
    public Platform Platform {
        get { return platform; }
        private set { platform = value; }
    }

    [SerializeField] bool useVibration = true;
    public bool UseVibration {
        get { return useVibration; }
        set { useVibration = value; OnSetVibration(); }
    }

    [Header("Object Layers")]

    public string playerLayer;
    public string playerBulletLayer;
    public string enemyLayer;
    public string enemyBulletLayer;


    public bool IsMobile {
        get {
            return Platform == Platform.Android || Platform == Platform.Iphone;
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
        if (UseVibration)
        {
#if UNITY_ANDROID
            Handheld.Vibrate();
#else

#endif
        }

        Save(); 
        OnVibrationChange.Invoke();
    }


    void GetPlatform()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                Platform = Platform.Android;
                break;

            case RuntimePlatform.IPhonePlayer:
                Platform = Platform.Iphone;
                break;

            case RuntimePlatform.WindowsPlayer:
                Platform = Platform.Windows;
                break;

            case RuntimePlatform.LinuxPlayer:
                Platform = Platform.Lunix;
                break;

            case RuntimePlatform.OSXPlayer:
                Platform = Platform.Mac;
                break;

            case RuntimePlatform.PS4:
                Platform = Platform.Playstation;
                break;

            case RuntimePlatform.XboxOne:
                Platform = Platform.Xbox;
                break;

            case RuntimePlatform.Switch:
                Platform = Platform.Switch;
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
        else
        {
            useVibration = true;
        }
    }
}

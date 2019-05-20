using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    [SerializeField] Enums.Platform platform;
    public Enums.Platform Platform {
        get { return platform; }
        private set { platform = value; }
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

    [SerializeField] AudioManager audio;
    public AudioManager Audio {
        get { return audio; }
        private set { audio = value; }
    }

    [SerializeField] ShipManager player;
    public ShipManager Player {
        get { return player; }
        private set { player = value; }
    }
    
    
    public bool IsMobile {
        get {
            return Platform == Enums.Platform.Android || Platform == Enums.Platform.Iphone;
        }
    }

    

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

        GetPlatform();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
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

}

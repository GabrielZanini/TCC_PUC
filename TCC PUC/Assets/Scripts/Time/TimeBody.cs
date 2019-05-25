using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeBody : MonoBehaviour
{
    public TimeController controller;
    public TimeBodyType bodyType = TimeBodyType.Bullet;

    public bool isActive = false;

    public List<MonoBehaviour> scriptsToDisable = new List<MonoBehaviour>();
    public List<GameObject> objectsToDisable = new List<GameObject>();

    public bool smoothPosition = false;

    public List<PointInTime> pointsInTime = new List<PointInTime>();
    private List<PointInTime> hiddenPointsInTime = new List<PointInTime>();

    [HideInInspector] public ObjectPool pool;

    [HideInInspector] public Vector3 spawnPosition;
    [HideInInspector] public Quaternion spawnRotation;

    [HideInInspector] public UnityEvent OnSpawn;
    [HideInInspector] public UnityEvent OnDespawn;

    [HideInInspector] public UnityEvent OnActivate;
    [HideInInspector] public UnityEvent OnDisactivate;

    [HideInInspector] public StatusBase status;
    [HideInInspector] public ScrollTiles scroll;
    [HideInInspector] public ParticleEffect particle;
    [HideInInspector] public Bullet bullet;
    [HideInInspector] public AudioManager audio;
    [HideInInspector] public Vector3 targetPosition;


    int pointIndex = 0;

    Collider[] colliders;

    //Aux Variebles
    PointInTime auxPointInTime;
    PointInTime previuos;
    PointInTime next;


    void Awake()
    {
        GetComponents();
    }

    void Start()
    {
        if (bodyType != TimeBodyType.System)
        {
            controller.AddTimebody(this);
        }
        
        AddListener();

        PoulateList();

        SetActive(isActive);
    }

    private void Update()
    {
        if (controller.IsRewinding && smoothPosition && isActive)
        {
            previuos = PreviuosPoint();
            next = NextPoint();

            if ((previuos == null || next == null) || !previuos.isActive || !next.isActive)
            {
                transform.position = targetPosition;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, 0.5f);
            }            
        }
    }

    void OnDestroy()
    {
        RemoveListener();
    }



    public void Despawn()
    {
        if (pool != null && isActive)
        {
            pool.Despawn(this);
            OnDespawn.Invoke();
        }
        else
        {
            SetActive(false);
        }
    }

    public void DestroyObject()
    {
        controller.RemoveTimebody(this);

        if (pool != null)
        {
            pool.DestroyObject(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }




    PointInTime PreviuosPoint()
    {
        if (pointIndex == 0)
        {
            return null;
        }
        else
        {
            return pointsInTime[pointIndex - 1];
        }
    }
    
    PointInTime NextPoint()
    {
        if (pointIndex == pointsInTime.Count - 1)
        {
            return null;
        }
        else
        {
            return pointsInTime[pointIndex + 1];
        }
    }


    // Listeners

    void AddListener()
    {
        //controller.OnRewind.AddListener(Rewind);
        //controller.OnRecord.AddListener(Record);
        //controller.OnOverload.AddListener(ClearList);

        controller.OnStartRewind.AddListener(DisableScripts);
        controller.OnStartRewind.AddListener(DisableCollider);

        controller.OnStopRewind.AddListener(Activate);

        GameManager.Instance.Level.OnRestart.AddListener(ClearList);
    }

    void RemoveListener()
    {
        //controller.OnRewind.RemoveListener(Rewind);
        //controller.OnRecord.RemoveListener(Record);
        //controller.OnOverload.RemoveListener(ClearList);

        controller.OnStartRewind.RemoveListener(DisableScripts);
        controller.OnStartRewind.RemoveListener(DisableCollider);

        controller.OnStopRewind.RemoveListener(Activate);

        GameManager.Instance.Level.OnRestart.RemoveListener(ClearList);
    }


    // Component Scripts

    void EnableScripts()
    {
        SetScriptsEnable(true);
    }

    void DisableScripts()
    {
        SetScriptsEnable(false);
    }

    void SetScriptsEnable(bool enable)
    {
        for (int i = 0; i < scriptsToDisable.Count; i++)
        {
            scriptsToDisable[i].enabled = enable;
        }
    }


    // Objects

    public void EnableObjects()
    {
        SetObjectsEnable(true);
    }

    public void DisableObjects()
    {
        SetObjectsEnable(false);
    }

    void SetObjectsEnable(bool enable)
    {
        for (int i = 0; i < objectsToDisable.Count; i++)
        {
            objectsToDisable[i].SetActive(enable);
        }
    }


    // Colliders

    void EnableCollider()
    {
        if (isActive)
        {
            SetColliderEnable(true);
        }        
    }

    void DisableCollider()
    {
        SetColliderEnable(false);
    }

    void SetColliderEnable(bool enable)
    {
        if (colliders != null)
        {
            for (int i=0; i<colliders.Length; i++)
            {
                colliders[i].enabled = enable;
            }
        }
    }


    // Time Methods

    public void Rewind()
    {
        if (bodyType == TimeBodyType.System)
        {
            return;
        }

        if (pointsInTime.Count > 0)
        {
            auxPointInTime = pointsInTime[0];
            pointsInTime.RemoveAt(0);
            //DeletePointinTimeAt(0);

            SetPointInTime(auxPointInTime);
        }
    }

    public void Record()
    {
        if (bodyType == TimeBodyType.System)
        {
            return;
        }

        if (pointsInTime.Count > controller.MaxPointsInTime)
        {
            auxPointInTime = pointsInTime[pointsInTime.Count - 1];
            
            pointsInTime.Remove(auxPointInTime);
            //DeletePointinTime(auxPointInTime);
        }
        else
        {
            //if (hiddenPointsInTime.Count > 0)
            //{
            //    auxPointInTime = ReUsePointInTime();
            //}
            //else
            //{
            //    auxPointInTime = GetNewPointInTime();
            //}

            auxPointInTime = GetNewPointInTime();
        }

        auxPointInTime.Save();

        pointsInTime.Insert(0, auxPointInTime);
    }


    // Set Point In Time

    public void SetPointInTime(int index)
    {
        if (bodyType == TimeBodyType.System)
        {
            return;
        }

        if (index < controller.MaxPointsInTime)
        {
            while (pointsInTime.Count <= index)
            {
                pointsInTime.Add(GetNewPointInTime(false));
            }
            
            pointIndex = index;
            SetPointInTime(pointsInTime[index]);
        }
    }

    void SetPointInTime(PointInTime pointInTime)
    {
        pointInTime.Load();
    }


    // PIT List

    public void ClearList()
    {
        while (pointsInTime.Count > 0)
        {
            pointsInTime.RemoveAt(0);
        }
    }

    public void ShortList(int size)
    {
        if (bodyType == TimeBodyType.System)
        {
            return;
        }

        if (size > 0)
        {
            while (pointsInTime.Count > size)
            {
                pointsInTime.RemoveAt(size - 1);
            }
        }        
        
    }
    
    void DeletePointinTimeAt(int index)
    {
        DeletePointinTime(pointsInTime[index]);
    }

    void DeletePointinTime(PointInTime pointInTime)
    {
        pointsInTime.Remove(pointInTime);
        hiddenPointsInTime.Add(pointInTime);
    }
    

    // Other Methods

    void PoulateList()
    {
        while (pointsInTime.Count < controller.CurrentPointInTime)
        {
            pointsInTime.Add(GetNewPointInTime(false));
        }
    }

    void Activate()
    {
        if (pool != null)
        {
            if (isActive)
            {
                pool.CheckLists(this);
            }
            else
            {
                Despawn();
            }            
        }
        else
        {
            SetActive(isActive);
        }
    } 

    void GetComponents()
    {
        colliders = GetComponentsInChildren<Collider>();

        status = GetComponent<StatusBase>();
        scroll = GetComponent<ScrollTiles>();
        particle = GetComponent<ParticleEffect>();
        bullet = GetComponent<Bullet>();
    }

    public void SetActive(bool active)
    {
        //Debug.Log(gameObject.name + " - TimeBody.SetActive - Active: " + active.ToString());

        isActive = active;

        SetScriptsEnable(active);
        SetObjectsEnable(active);
        SetColliderEnable(active);

        if (active)
        {
            OnActivate.Invoke();
        }
        else
        {
            OnDisactivate.Invoke();
        }
    }    

    private PointInTime GetNewPointInTime()
    {
        if (bodyType == TimeBodyType.Bullet)
        {
            auxPointInTime = new PointInTimeBullet();
        }
        else if (bodyType == TimeBodyType.Missile)
        {
            auxPointInTime = new PointInTimeMissile();
        }
        else if (bodyType == TimeBodyType.Ship)
        {
            auxPointInTime = new PointInTimeShip();
        }
        else if (bodyType == TimeBodyType.Asteroid)
        {
            auxPointInTime = new PointInTimeAsteroid();
        }
        else if (bodyType == TimeBodyType.PickUp)
        {
            auxPointInTime = new PointinTimePickUp();
        }
        else if (bodyType == TimeBodyType.Background)
        {
            auxPointInTime = new PointInTimeBackground();
        }
        else if (bodyType == TimeBodyType.Particle)
        {
            auxPointInTime = new PointInTimeParticle();
        }
        else
        {
            auxPointInTime = new PointInTime();
        }

        auxPointInTime.timebody = this;

        return auxPointInTime;
    }


    private PointInTime GetNewPointInTime(bool active)
    {
        auxPointInTime = GetNewPointInTime();

        auxPointInTime.isActive = active;

        return auxPointInTime;
    }


    private PointInTime ReUsePointInTime()
    {
        PointInTime pointInTime = hiddenPointsInTime[0];

        hiddenPointsInTime.RemoveAt(0);

        return pointInTime;
    }
}

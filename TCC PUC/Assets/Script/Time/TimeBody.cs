using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeBody : MonoBehaviour
{
    public Enums.TimeBodyType bodyType = Enums.TimeBodyType.Bullet;

    public bool isActive = false;

    public List<MonoBehaviour> scriptsToDisable = new List<MonoBehaviour>();
    public List<GameObject> objectsToDisable = new List<GameObject>();
    public List<PointInTime> pointsInTime = new List<PointInTime>();

    public ObjectPool pool;

    [HideInInspector] public UnityEvent OnActivate;
    [HideInInspector] public UnityEvent OnDisactivate;

    [HideInInspector] public StatusBase status;
    [HideInInspector] public ScrollBackground scroll;

    Collider[] colliders;
    PointInTime auxPointInTime;
    



    void Awake()
    {
        colliders = GetComponentsInChildren<Collider>();
        status = GetComponent<StatusBase>();
        scroll = GetComponent<ScrollBackground>();
    }

    void Start()
    {
        TimeController.Instance.AddTimebody(this);

        //TimeController.Instance.OnRewind.AddListener(Rewind);
        //TimeController.Instance.OnRecord.AddListener(Record);
        //TimeController.Instance.OnOverload.AddListener(ClearList);

        TimeController.Instance.OnStartRewind.AddListener(DisableScripts);
        TimeController.Instance.OnStartRewind.AddListener(DisableCollider);

        TimeController.Instance.OnStopRewind.AddListener(Activate);

        SetActive(isActive);
    }

    void OnDestroy()
    {
        //TimeController.Instance.OnRewind.RemoveListener(Rewind);
        //TimeController.Instance.OnRecord.RemoveListener(Record);
        //TimeController.Instance.OnOverload.RemoveListener(ClearList);

        TimeController.Instance.OnStartRewind.RemoveListener(DisableScripts);
        TimeController.Instance.OnStartRewind.RemoveListener(DisableCollider);

        TimeController.Instance.OnStopRewind.RemoveListener(Activate);
    }


    public void Despawn()
    {
        if (pool != null && isActive)
        {
            pool.Despawn(this);
        }
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
        if (pointsInTime.Count > 0)
        {
            auxPointInTime = pointsInTime[0];
            pointsInTime.RemoveAt(0);

            SetPointInTime(auxPointInTime);
        }
    }

    public void Record()
    {
        if (pointsInTime.Count > TimeController.Instance.MaxPointsInTime)
        {
            auxPointInTime = pointsInTime[pointsInTime.Count - 1];
            pointsInTime.Remove(auxPointInTime);
        }
        else
        {
            auxPointInTime = GetNewPointInTime();
        }

        auxPointInTime.Save();

        pointsInTime.Insert(0, auxPointInTime);
    }


    // Set Point In Time

    public void SetPointInTime(int index)
    {
        if (index < pointsInTime.Count)
        {
            SetPointInTime(pointsInTime[index]);
        }
    }

    public void SetPointInTime(PointInTime pointInTime)
    {
        pointInTime.Load();
    }


    // PIT List

    public void ClearList()
    {
        pointsInTime.Clear();
    }

    public void ShortList(int size)
    {
        while (pointsInTime.Count > size)
        {
            pointsInTime.RemoveAt(size);
        }
    }
    

    // Other Methods

    void Activate()
    {
        if (pool != null)
        {
            Despawn();
        }
        else
        {
            SetActive(isActive);
        }
    } 

    public void SetActive(bool active)
    {
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
        
        isActive = active;
    }    

    private PointInTime GetNewPointInTime()
    {
        if (bodyType == Enums.TimeBodyType.Bullet)
        {
            auxPointInTime = new PointInTimeBullet();
        }
        else if (bodyType == Enums.TimeBodyType.Missile)
        {
            auxPointInTime = new PointInTimeMissile();
        }
        else if (bodyType == Enums.TimeBodyType.Ship)
        {
            auxPointInTime = new PointInTimeShip();
        }
        else if (bodyType == Enums.TimeBodyType.Asteroid)
        {
            auxPointInTime = new PointInTimeAsteroid();
        }
        else if (bodyType == Enums.TimeBodyType.Collectable)
        {
            auxPointInTime = new PointinTimeCollectable();
        }
        else if (bodyType == Enums.TimeBodyType.Background)
        {
            auxPointInTime = new PointInTimeBackground();
        }
        else
        {
            auxPointInTime = new PointInTime();
        }

        auxPointInTime.timebody = this;

        return auxPointInTime;
    }
}

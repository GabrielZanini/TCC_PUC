using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeBody : MonoBehaviour
{
    public Enums.TimeBodyType bodyType = Enums.TimeBodyType.Projectile;

    public bool isActive = false;

    public List<MonoBehaviour> scriptsToDisable = new List<MonoBehaviour>();
    public List<GameObject> objectsToDisable = new List<GameObject>();
    public ObjectPool pool;

    [HideInInspector] public UnityEvent OnActivate;
    [HideInInspector] public UnityEvent OnDisactivate;

    Collider[] colliders;
    StatusBase status;

    int hp = 0;

    List<PointInTime> pointsInTime = new List<PointInTime>();


    void Awake()
    {
        colliders = GetComponentsInChildren<Collider>();
        status = GetComponent<StatusBase>();
    }

    void Start()
    {
        if (bodyType != Enums.TimeBodyType.System)
        {
            TimeController.Instance.OnRewind.AddListener(Rewind);
            TimeController.Instance.OnRecord.AddListener(Record);
            TimeController.Instance.OnOverload.AddListener(ClearList);
        }

        TimeController.Instance.OnStartRewind.AddListener(DisableScripts);
        TimeController.Instance.OnStartRewind.AddListener(DisableCollider);
        TimeController.Instance.OnStopRewind.AddListener(EnableScripts);
        TimeController.Instance.OnStopRewind.AddListener(EnableCollider);

        SetActive(isActive);
    }

    void OnDestroy()
    {
        if (bodyType != Enums.TimeBodyType.System)
        {
            TimeController.Instance.OnRewind.RemoveListener(Rewind);
            TimeController.Instance.OnRecord.RemoveListener(Record);
            TimeController.Instance.OnOverload.RemoveListener(ClearList);
        }

        TimeController.Instance.OnStartRewind.RemoveListener(DisableScripts);
        TimeController.Instance.OnStartRewind.RemoveListener(DisableCollider);
        TimeController.Instance.OnStopRewind.RemoveListener(EnableScripts);
        TimeController.Instance.OnStopRewind.RemoveListener(EnableCollider);
    }


    public void Despawn()
    {
        if (pool != null && isActive)
        {
            pool.Despawn(this);
        }
    }


    // Other Scripts

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

    void EnableObjects()
    {
        SetObjectsEnable(true);
    }

    void DisableObjects()
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

    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];

            transform.position = pointInTime.position;
            //transform.rotation = pointInTime.rotation;
            
            //if (status != null)
            //{
            //    status.currentHp = pointInTime.hp;
            //}

            if (pointInTime.isActive)
            {
                EnableObjects();
            }
            else
            {
                DisableObjects();
            }

            pointsInTime.RemoveAt(0);
        }
    }

    void Record()
    {
        if (pointsInTime.Count > Mathf.Round(TimeController.Instance.maxTimeRewind / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
        
        if (status != null)
        {
            hp = status.currentHp;
        }

        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, isActive, hp));
    }

    void ClearList()
    {
        pointsInTime.Clear();
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
    
}

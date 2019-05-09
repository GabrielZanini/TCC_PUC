using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public Enums.TimeBodyType bodyType = Enums.TimeBodyType.Projectile;

    public bool isActive = false;

    public List<MonoBehaviour> scriptsToDisable = new List<MonoBehaviour>();
    public List<GameObject> objectsToDisable = new List<GameObject>();

    public GameObject mesh;

    Collider collider;
    StatusBase status;
    int hp = 0;

    List<PointInTime> pointsInTime = new List<PointInTime>();

    void Awake()
    {
        collider = GetComponent<Collider>();
        status = GetComponent<StatusBase>();
    }

    void Start()
    {
        TimeController.Instance.OnRewind.AddListener(Rewind);
        TimeController.Instance.OnRecord.AddListener(Record);

        TimeController.Instance.OnStartRewind.AddListener(DisableScripts);
        TimeController.Instance.OnStopRewind.AddListener(EnableScripts);

        TimeController.Instance.OnOverload.AddListener(ClearList);
    }

    void Destroy()
    {
        TimeController.Instance.OnRewind.RemoveListener(Rewind);
        TimeController.Instance.OnRecord.RemoveListener(Record);

        TimeController.Instance.OnStartRewind.RemoveListener(DisableScripts);
        TimeController.Instance.OnStopRewind.RemoveListener(EnableScripts);

        TimeController.Instance.OnOverload.RemoveListener(ClearList);
    }


    void DisableScripts()
    {
        for (int i=0; i < scriptsToDisable.Count; i++)
        {
            scriptsToDisable[i].enabled = false;
        }

        for (int i = 0; i < objectsToDisable.Count; i++)
        {
            objectsToDisable[i].SetActive(false);
        }

        if (collider != null)
        {
            collider.enabled = false;
        }
    }

    void EnableScripts()
    {
        for (int i = 0; i < scriptsToDisable.Count; i++)
        {
            scriptsToDisable[i].enabled = true;
        }

        for (int i = 0; i < objectsToDisable.Count; i++)
        {
            objectsToDisable[i].SetActive(true);
        }

        if (collider != null)
        {
            collider.enabled = true;
        }
    }


    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];

            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;

            if (mesh != null)
            {
                mesh.SetActive(pointInTime.isActive);
            }

            if (status != null)
            {
                status.currentHp = pointInTime.hp;
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
        if (active)
        {
            EnableScripts();
        }
        else
        {
            DisableScripts();
        }

        if (mesh != null)
        {
            mesh.SetActive(active);
        }
        
        isActive = active;
    }
}

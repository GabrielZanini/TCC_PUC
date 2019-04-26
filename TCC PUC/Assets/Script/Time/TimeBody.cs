using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public bool isBullet = false;

    List<PointInTime> pointsInTime = new List<PointInTime>();

    void Awake()
    {
        
    }

    void Start()
    {
        TimeController.Instance.OnRewind.AddListener(Rewind);
        TimeController.Instance.OnRecord.AddListener(Record);
    }

    void Destroy()
    {
        TimeController.Instance.OnRewind.RemoveListener(Rewind);
        TimeController.Instance.OnRecord.RemoveListener(Record);
    }



    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];

            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;

            pointsInTime.RemoveAt(0);
        }
    }

    void Record()
    {
        if (pointsInTime.Count > Mathf.Round(TimeController.Instance.maxTimeRewind / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }
}

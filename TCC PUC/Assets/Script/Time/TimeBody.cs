using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public bool isRewinding = false;

    List<PointInTime> pointsInTime = new List<PointInTime>();

    void Start()
    {

    }

    void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartRewind();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StoptRewind();
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
    }
    
    public void StoptRewind()
    {
        isRewinding = false;
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
        else
        {
            StoptRewind();
        }

    }

    void Record()
    {
        if (pointsInTime.Count > Mathf.Round(3f / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }
}

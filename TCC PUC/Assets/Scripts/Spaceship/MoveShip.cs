using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShip : MonoBehaviour
{
    public float speed = 1f;
    public float angularSpeed = 180f;
    public float smoothness = 0.5f;

    
    public Vector3 startPosition;


    bool useLimits = false;
    bool useStartPosition = false;

    Vector3 maxPosition;
    Vector3 minPosition;

    Vector3 newShipPosition;
    

    void Start()
    {
        AddListeners();

        if (useStartPosition)
        {
            StartShip();
        }
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }



    // Listeners

    void AddListeners()
    {
        GameManager.Instance.Level.OnBeforeStart.AddListener(StartShip);
    }

    void RemoveListeners()
    {
        GameManager.Instance.Level.OnBeforeStart.RemoveListener(StartShip);
    }


    public void MoveHorizontal(float horizontal)
    {
        newShipPosition = transform.localPosition + (Vector3.right * horizontal * speed * Time.deltaTime);
        MoveToPosition(newShipPosition);
    }

    public void MoveVertical(float vertical)
    {
        newShipPosition = transform.localPosition + (Vector3.up * vertical * speed * Time.deltaTime);
        MoveToPosition(newShipPosition);
    }
    
    public void MoveToPosition(Vector3 newPositon)
    {
        if (useLimits)
        {
            newShipPosition = CorrectNewPosition(newPositon);
        }
        
        transform.localPosition = Vector3.Lerp(transform.position, newShipPosition, smoothness);
    }

    Vector3 CorrectNewPosition(Vector3 newPosition)
    {
        newPosition = Vector3.Max(newPosition, minPosition);
        newPosition = Vector3.Min(newPosition, maxPosition);

        return newPosition;
    }


    public void Rotate(float rotation)
    {
        transform.rotation = transform.rotation * Quaternion.Euler(0f, rotation * angularSpeed * Time.deltaTime, 0f);
    }


    public void SetLimits(Vector3 min, Vector3 max)
    {
        useLimits = true;
        minPosition = min;
        maxPosition = max;
    }

    public void ClearLimits()
    {
        useLimits = false;
    }
    
    public void SetStartPosition(Vector3 position)
    {
        useStartPosition = true;
        startPosition = position;
        StartShip();
    }

    public void ClearStartposition()
    {
        useStartPosition = false;
    }

    private void StartShip()
    {
        transform.position = startPosition;
    }
}

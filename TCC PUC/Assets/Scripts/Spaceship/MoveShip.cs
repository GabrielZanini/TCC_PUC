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
    Vector3 maxLocalPosition;
    Vector3 minLocalPosition;

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
        GameManager.Instance.Level.OnMenu.AddListener(StartShip);
        GameManager.Instance.Level.OnStart.AddListener(StartShip);
    }

    void RemoveListeners()
    {
        GameManager.Instance.Level.OnMenu.RemoveListener(StartShip);
        GameManager.Instance.Level.OnStart.RemoveListener(StartShip);
    }


    public void MoveHorizontal(float horizontal)
    {
        newShipPosition = transform.localPosition + (Vector3.right * horizontal * speed * Time.deltaTime);
        MoveToLocalPosition(newShipPosition);
    }

    public void MoveVertical(float vertical)
    {
        newShipPosition = transform.localPosition + (Vector3.forward * vertical * speed * Time.deltaTime);
        MoveToLocalPosition(newShipPosition);
    }

    public void MoveToPosition(Vector3 newPositon)
    {
        if (useLimits)
        {
            newShipPosition = CorrectNewPosition(newPositon);
        }

        transform.position = Vector3.Lerp(transform.position, newShipPosition, smoothness);
    }

    public void MoveToLocalPosition(Vector3 newPositon)
    {
        if (useLimits)
        {
            newShipPosition = CorrectNewLocalPosition(newPositon);
        }

        transform.localPosition = Vector3.Lerp(transform.position, newShipPosition, smoothness);
    }

    Vector3 CorrectNewLocalPosition(Vector3 newPosition)
    {
        newPosition = Vector3.Max(newPosition, minLocalPosition);
        newPosition = Vector3.Min(newPosition, maxLocalPosition);

        return newPosition;
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


    public void SetLimits(Vector3 min, Vector3 max, Margin margin)
    {
        useLimits = true;
        minPosition = min + new Vector3(margin.all + margin.left, 0, margin.all + margin.bottom);
        maxPosition = max - new Vector3(margin.all + margin.right, 0, margin.all + margin.top);

        minLocalPosition = minPosition + (transform.position - transform.localPosition);
        maxLocalPosition = maxPosition + (transform.position - transform.localPosition);
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
